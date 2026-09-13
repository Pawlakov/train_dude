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
                var enriched = new SegmentCreatedWithReferences(e.Data, aEnriched, bEnriched);

                slice.ReplaceEvent(e, enriched);
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

    public void Apply(SettingsNamingPolicySet e, SegmentReadModel aggregate)
    {
    }
}