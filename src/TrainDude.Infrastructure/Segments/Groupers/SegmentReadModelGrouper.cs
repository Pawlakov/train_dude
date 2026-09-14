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

internal sealed class SegmentReadModelGrouper
    : IAggregateGrouper<Guid>
{
    public async Task Group(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        var links = await session.Query<SegmentAggregate>()
            .ToListAsync();

        var segmentIdsByStation = links
            .SelectMany(l => new[]
            {
                (StationId: l.A.Id, SegmentId: l.Id),
                (StationId: l.B.Id, SegmentId: l.Id),
            })
            .GroupBy(x => x.StationId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.SegmentId).ToList());

        var segmentIds = await session.Events
            .QueryRawEventDataOnly<SegmentCreated>()
            .Select(x => x.SegmentId)
            .Distinct()
            .ToListAsync();

        foreach (var e in events.OrderBy(x => x.Sequence))
        {
            if (e.Data is ISegmentEvent segmentEvent)
            {
                grouping.AddEvent(segmentEvent.SegmentId, e);
            }
            else if (e.Data is StationLocationSet)
            {
                this.GroupLocationSet(session, (IEvent<StationLocationSet>)e, grouping, segmentIdsByStation);
            }
            else if (e.Data is SettingsNamingPolicySet)
            {
                this.GroupNamingPolicySet(session, (IEvent<SettingsNamingPolicySet>)e, grouping, segmentIds);
            }
        }
    }

    private void GroupLocationSet(IQuerySession session, IEvent<StationLocationSet> locationSetEvent, IEventGrouping<Guid> grouping, Dictionary<Guid, List<Guid>> segmentIdsByStation)
    {
        if (segmentIdsByStation.TryGetValue(locationSetEvent.Data.StationId, out var segmentIds))
        {
            foreach (var segmentId in segmentIds)
            {
                grouping.AddEvent(segmentId, locationSetEvent);
            }
        }
    }

    private void GroupNamingPolicySet(IQuerySession session, IEvent<SettingsNamingPolicySet> namingPolicySetEvent, IEventGrouping<Guid> grouping, IEnumerable<Guid> segmentIds)
    {
        foreach (var segmentId in segmentIds)
        {
            grouping.AddEvent(segmentId, namingPolicySetEvent);
        }
    }
}