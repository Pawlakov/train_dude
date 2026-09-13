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
        var settingsReference = await querySession.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id, cancellation);

        // works wonderfully but good luck reading this
        var stationIds = group.Slices
            .SelectMany(x => x.Events().OfType<IEvent<SegmentCreated>>()
                .SelectMany(y => new[] { y.Data.A.Id, y.Data.B.Id })
                .Concat(x.Snapshot != null ? new[] { x.Snapshot.A.Id, x.Snapshot.B.Id } : []))
            .ToList();

        var stations = await querySession.LoadManyAsync<SegmentStationReference>(cancellation, stationIds);
        var stationsById = stations.ToDictionary(s => s.Id, s => s);

        foreach (var slice in group.Slices)
        {
            var namingPolicySetEvent = slice.Events().OfType<IEvent<SettingsNamingPolicySet>>().OrderByDescending(x => x.Sequence).FirstOrDefault();
            var createdEvent = slice.Events().OfType<IEvent<SegmentCreated>>().SingleOrDefault();

            if (createdEvent is null)
            {
                if (namingPolicySetEvent is not null)
                {
                    var nameSelector = StationNameResolver.GetNameSelector(namingPolicySetEvent.Data.NamingPolicy);
                    var aName = nameSelector(stationsById[slice.Snapshot.A.Id]);
                    var bName = nameSelector(stationsById[slice.Snapshot.B.Id]);
                    var enriched = new SettingsNamingPolicySetWithReferences(namingPolicySetEvent.Data, aName, bName);

                    slice.ReplaceEvent(namingPolicySetEvent, enriched);
                }
            }
            else
            {
                var a = stationsById[createdEvent.Data.A.Id];
                var b = stationsById[createdEvent.Data.B.Id];

                var policy = namingPolicySetEvent switch
                {
                    not null => namingPolicySetEvent.Data.NamingPolicy,
                    null => settingsReference?.NamingPolicy ?? NamingPolicy.Modern,
                };

                var nameSelector = StationNameResolver.GetNameSelector(policy);
                var aEnriched = new SegmentEndReference(createdEvent.Data.A.Id, createdEvent.Data.A.Axle, createdEvent.Data.A.Pole, a.Location, nameSelector(a));
                var bEnriched = new SegmentEndReference(createdEvent.Data.B.Id, createdEvent.Data.B.Axle, createdEvent.Data.B.Pole, b.Location, nameSelector(b));
                var enriched = new SegmentCreatedWithReferences(createdEvent.Data, aEnriched, bEnriched);

                slice.ReplaceEvent(createdEvent, enriched);
            }
        }
    }

    public void Apply(IEvent<SegmentCreatedWithReferences> e, SegmentReadModel aggregate)
    {
        aggregate.Id = e.Data.Event.SegmentId;
        aggregate.NominalLength = e.Data.Event.NominalLength;
        aggregate.Tracks = e.Data.Event.Tracks;
        aggregate.A = e.Data.A;
        aggregate.B = e.Data.B;
        aggregate.Course = [];

        aggregate.Haversine = ComputeHaversine(aggregate);
    }

    public void Apply(IEvent<SegmentCourseSet> e, SegmentReadModel aggregate)
    {
        aggregate.Course = e.Data.Course.ToList();

        aggregate.Haversine = ComputeHaversine(aggregate);
    }

    public void Apply(IEvent<StationLocationSet> e, SegmentReadModel aggregate)
    {
        if (aggregate.A == default || aggregate.B == default)
        {
            // event applied out of order
            // SegmentCreatedWithReferences should hopefully handle the location change
            return;
        }

        if (aggregate.A.Id == e.Data.StationId)
        {
            aggregate.A = aggregate.A with { Location = e.Data.Location };
        }

        if (aggregate.B.Id == e.Data.StationId)
        {
            aggregate.B = aggregate.B with { Location = e.Data.Location };
        }

        aggregate.Haversine = ComputeHaversine(aggregate);
    }

    public void Apply(IEvent<SettingsNamingPolicySetWithReferences> e, SegmentReadModel aggregate)
    {
        aggregate.A = aggregate.A with { Name = e.Data.AName };
        aggregate.B = aggregate.B with { Name = e.Data.BName };
    }

    public void Apply(IEvent<SettingsNamingPolicySet> e, SegmentReadModel aggregate)
    {
        // intentionally no-op
        // any meaningful naming policy changes should be handled with SegmentCreatedWithReferences or SettingsNamingPolicySetWithReferences
    }

    private static double? ComputeHaversine(SegmentReadModel aggregate)
    {
        return (aggregate.A.Location, aggregate.B.Location) switch
        {
            ({ } aLoc, { } bLoc) => aggregate.Course.Prepend(aLoc).Append(bLoc).Haversine(),
            _ => null,
        };
    }
}