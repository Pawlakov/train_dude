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
using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Stations.Domain.Events;

public sealed class SegmentReadModelGrouper
    : IAggregateGrouper<Guid>
{
    public async Task Group(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        foreach (var e in events)
        {
            if (e.Data is ISegmentEvent segmentEvent)
            {
                grouping.AddEvent(segmentEvent.SegmentId, e);
            }
            else if (e.Data is StationLocationSet)
            {
                await this.GroupLocationSet(session, (IEvent<StationLocationSet>)e, grouping);
            }
            else if (e.Data is SettingsNamingPolicySet)
            {
                await this.GroupNamingPolicySet(session, (IEvent<SettingsNamingPolicySet>)e, grouping);
            }
        }
    }

    private async Task GroupLocationSet(IQuerySession session, IEvent<StationLocationSet> locationSetEvent, IEventGrouping<Guid> grouping)
    {
        var stationId = locationSetEvent.Data.Id;

        var links = await session.Query<SegmentAggregate>()
            .Where(x => x.A.Id == stationId || x.B.Id == stationId)
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

        if (segmentIdsByStation.TryGetValue(locationSetEvent.Data.Id, out var segmentIds))
        {
            foreach (var segmentId in segmentIds)
            {
                grouping.AddEvent(segmentId, locationSetEvent);
            }
        }
    }

    private async Task GroupNamingPolicySet(IQuerySession session, IEvent<SettingsNamingPolicySet> namingPolicySetEvent, IEventGrouping<Guid> grouping)
    {
        var stationIds = await session.Events
            .QueryRawEventDataOnly<StationCreated>()
            .Select(x => x.StationId)
            .Distinct()
            .ToListAsync();

        foreach (var stationId in stationIds)
        {
            grouping.AddEvent(stationId, namingPolicySetEvent);
        }
    }
}