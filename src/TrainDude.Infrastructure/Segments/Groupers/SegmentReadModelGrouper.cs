// <copyright file="SegmentReadModelGrouper.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Segments.Groupers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JasperFx.Events;
using JasperFx.Events.Grouping;

using Marten;
using Marten.Events.Aggregation;

using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Stations.Domain.Events;

public sealed class SegmentReadModelGrouper
    : IAggregateGrouper<Guid>
{
    public async Task Group(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        await this.GroupLocationSet(session, events, grouping);
        await this.GroupNamingPolicySet(session, events, grouping);
    }

    private async Task GroupLocationSet(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        var locationSetEvents = events.OfType<IEvent<StationLocationSet>>().ToList();
        if (locationSetEvents.Count == 0)
        {
            return;
        }

        var stationIds = locationSetEvents.Select(e => e.Data.Id).Distinct().ToList();

        var links = await session.Query<SegmentAggregate>()
            .Where(x => stationIds.Contains(x.A.Id) || stationIds.Contains(x.B.Id))
            .Select(x => new { x.Id, x.A, x.B })
            .ToListAsync();

        var segmentIdsByStation = links
            .SelectMany(l => new[]
            {
                (StationId: l.A.Id, SegmentId: l.Id),
                (StationId: l.B.Id, SegmentId: l.Id),
            })
            .GroupBy(x => x.StationId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.SegmentId).ToList());

        foreach (var e in locationSetEvents)
        {
            if (!segmentIdsByStation.TryGetValue(e.Data.Id, out var segmentIds))
            {
                continue;
            }

            foreach (var segmentId in segmentIds)
            {
                grouping.AddEvent(segmentId, e);
            }
        }
    }

    private async Task GroupNamingPolicySet(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        var policyEvents = events.OfType<IEvent<SettingsNamingPolicySet>>().ToList();
        if (policyEvents.Count == 0)
        {
            return;
        }

        var allSegmentIds = await session.Query<SegmentAggregate>()
            .Select(x => x.Id)
            .ToListAsync();

        foreach (var e in policyEvents)
        {
            foreach (var segmentId in allSegmentIds)
            {
                grouping.AddEvent(segmentId, e);
            }
        }
    }
}