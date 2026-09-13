// <copyright file="SegmentReadModelProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Segments.Projections;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;
using JasperFx.Events.Grouping;

using Marten;
using Marten.Events.Projections;

using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Infrastructure.Segments.Events;
using TrainDude.Infrastructure.Segments.Groupers;

public sealed class SegmentReadModelProjection
    : MultiStreamProjection<SegmentReadModel, Guid>
{
    public SegmentReadModelProjection()
    {
        this.TransformsEvent<ISegmentEvent>();
        this.CustomGrouping(new SegmentReadModelGrouper());
    }

    public override async Task EnrichEventsAsync(SliceGroup<SegmentReadModel, Guid> group, IQuerySession querySession, CancellationToken cancellation)
    {
        foreach (var slice in group.Slices)
        {
            var namingPolicySetEvent = slice.Events().OfType<IEvent<SettingsNamingPolicySet>>().OrderByDescending(x => x.Sequence).FirstOrDefault();
            var createdEvent = slice.Events().OfType<IEvent<SegmentCreated>>().SingleOrDefault();

            if (createdEvent is null)
            {
                if (namingPolicySetEvent is not null)
                {
                    var a = await querySession.LoadAsync<SegmentStationReference>(slice.Snapshot.A.Id, cancellation);
                    var b = await querySession.LoadAsync<SegmentStationReference>(slice.Snapshot.B.Id, cancellation);

                    var nameSelector = StationNameResolver.GetNameSelector(namingPolicySetEvent.Data.NamingPolicy);
                    var aName = nameSelector(a);
                    var bName = nameSelector(b);
                    var enriched = new SettingsNamingPolicySetWithReferences(namingPolicySetEvent.Data, aName, bName);

                    slice.ReplaceEvent(namingPolicySetEvent, enriched);
                }
            }
            else
            {
                var stationIds = new[] { createdEvent.Data.A.Id, createdEvent.Data.B.Id };
                var stations = await querySession.LoadManyAsync<SegmentStationReference>(cancellation, stationIds);
                var stationsById = stations.ToDictionary(s => s.Id, s => s);

                var a = stationsById[createdEvent.Data.A.Id];
                var b = stationsById[createdEvent.Data.B.Id];

                var policy = namingPolicySetEvent switch
                {
                    not null => namingPolicySetEvent.Data.NamingPolicy,
                    null => (await querySession.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id, cancellation))?.NamingPolicy ?? NamingPolicy.Modern,
                };

                var nameSelector = StationNameResolver.GetNameSelector(policy);
                var aEnriched = new SegmentEndReference(createdEvent.Data.A.Id, createdEvent.Data.A.Axle, createdEvent.Data.A.Pole, a.Location, nameSelector(a));
                var bEnriched = new SegmentEndReference(createdEvent.Data.B.Id, createdEvent.Data.B.Axle, createdEvent.Data.B.Pole, b.Location, nameSelector(b));
                var enriched = new SegmentCreatedWithReferences(createdEvent.Data, aEnriched, bEnriched);

                slice.ReplaceEvent(createdEvent, enriched);
            }
        }
    }

    public void Apply(SegmentCreatedWithReferences e, SegmentReadModel aggregate)
    {
        aggregate.Id = e.Event.Id;
        aggregate.NominalLength = e.Event.NominalLength;
        aggregate.Tracks = e.Event.Tracks;
        aggregate.A = e.A;
        aggregate.B = e.B;
        aggregate.Course = [];

        aggregate.Haversine = (aggregate.A.Location, aggregate.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => aggregate.Course.Prepend(aLocation).Append(bLocation).Haversine(),
            _ => null,
        };
    }

    public void Apply(SegmentCourseSet e, SegmentReadModel aggregate)
    {
        aggregate.Course = e.Course.ToList();

        aggregate.Haversine = (aggregate.A.Location, aggregate.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => aggregate.Course.Prepend(aLocation).Append(bLocation).Haversine(),
            _ => null,
        };
    }

    public void Apply(StationLocationSet e, SegmentReadModel aggregate)
    {
        if (aggregate.A.Id == e.Id)
        {
            aggregate.A = aggregate.A with { Location = e.Location };
        }

        if (aggregate.B.Id == e.Id)
        {
            aggregate.B = aggregate.B with { Location = e.Location };
        }

        aggregate.Haversine = (aggregate.A.Location, aggregate.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => aggregate.Course.Prepend(aLocation).Append(bLocation).Haversine(),
            _ => null,
        };
    }

    public void Apply(SettingsNamingPolicySetWithReferences e, SegmentReadModel aggregate)
    {
        aggregate.A = aggregate.A with { Name = e.AName };
        aggregate.B = aggregate.B with { Name = e.BName };
    }
}