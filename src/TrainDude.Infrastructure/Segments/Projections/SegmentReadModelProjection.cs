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
using TrainDude.Features.Segments.ReadModels.Events;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Stations.Domain.Events;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Infrastructure.Segments.Groupers;
using TrainDude.Shared.Enums;

public sealed class SegmentReadModelProjection
    : MultiStreamProjection<SegmentReadModel, Guid>
{
    public SegmentReadModelProjection()
    {
        this.Identity<SegmentCreated>(e => e.Id);
        this.Identity<SegmentCourseSet>(e => e.Id);

        this.CustomGrouping(new SegmentReadModelGrouper());
    }

    public override async Task EnrichEventsAsync(SliceGroup<SegmentReadModel, Guid> group, IQuerySession querySession, CancellationToken cancellation)
    {
        var createdEvents = group.Slices
            .SelectMany(slice => slice.Events().OfType<IEvent<SegmentCreated>>())
            .ToArray();

        if (createdEvents.Length == 0)
        {
            return;
        }

        var stationIds = createdEvents
            .SelectMany(e => new[] { e.Data.A.Id, e.Data.B.Id })
            .Distinct()
            .ToArray();

        var stations = await querySession.LoadManyAsync<SegmentStationReference>(cancellation, stationIds);
        var settings = await querySession.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id, cancellation);

        var stationsById = stations.ToDictionary(s => s.Id, s => s);
        var policy = settings?.NamingPolicy ?? NamingPolicy.Modern;
        var nameSelector = StationNameResolver.GetNameSelector(policy);

        foreach (var slice in group.Slices)
        {
            foreach (var e in slice.Events().OfType<IEvent<SegmentCreated>>().ToArray())
            {
                var a = stationsById[e.Data.A.Id];
                var b = stationsById[e.Data.B.Id];
                var aEnriched = new SegmentEndReference(e.Data.A.Id, e.Data.A.Axle, e.Data.A.Pole, a.Location, nameSelector(a));
                var bEnriched = new SegmentEndReference(e.Data.B.Id, e.Data.B.Axle, e.Data.B.Pole, b.Location, nameSelector(b));
                var enriched = new SegmentCreatedWithReferences(e.Data.Id, e.Data.When, e.Data.NominalLength, e.Data.Tracks, aEnriched, bEnriched);

                slice.ReplaceEvent(e, enriched);
            }
        }
    }

    public void Apply(SegmentCreatedWithReferences e, SegmentReadModel item)
    {
        double? haversine = (e.A.Location, e.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => aLocation.Haversine(bLocation),
            _ => null,
        };

        item.Id = e.Id;
        item.NominalLength = e.NominalLength;
        item.Haversine = haversine;
        item.A = e.A;
        item.B = e.B;

        item.Version++;
    }

    public void Apply(SegmentCourseSet e, SegmentReadModel item)
    {
        item.Course = e.Course.ToList();

        item.Haversine = (item.A.Location, item.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => e.Course.Prepend(aLocation).Append(bLocation).Haversine(),
            _ => null,
        };

        this.Version++;
    }

    public void Apply(StationLocationSet e, SegmentReadModel item)
    {
        if (item.A.Id == e.Id)
        {
            item.A = item.A with { Location = e.Location };
        }

        if (item.B.Id == e.Id)
        {
            item.B = item.B with { Location = e.Location };
        }
    }

    public void Apply(SettingsNamingPolicySet e, SegmentReadModel item)
    {
        throw new NotImplementedException();
    }
}