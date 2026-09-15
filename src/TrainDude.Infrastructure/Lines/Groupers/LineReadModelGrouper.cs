// <copyright file="LineReadModelGrouper.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Groupers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JasperFx.Events;
using JasperFx.Events.Grouping;

using Marten;
using Marten.Events.Aggregation;

using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Trips.Domain.Events;

internal sealed class LineReadModelGrouper
    : IAggregateGrouper<Guid>
{
    public async Task Group(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        var links = await session.Query<LineAggregate>()
            .ToListAsync();

        var lineIdsByTrip = links
            .SelectMany(x => x.Trips.Select(y => new { TripId = y, LineId = x.Id }))
            .GroupBy(x => x.TripId)
            .ToDictionary(x => x.Key, x => x.Select(y => y.LineId).ToList());

        var lineIdsBySegment = links
            .SelectMany(x => x.Segments.Select(y => new { SegmentId = y, LineId = x.Id }))
            .GroupBy(x => x.SegmentId)
            .ToDictionary(x => x.Key, x => x.Select(y => y.LineId).ToList());

        var lineIds = await session.Events
            .QueryRawEventDataOnly<LineCreated>()
            .Select(x => x.LineId)
            .Distinct()
            .ToListAsync();

        foreach (var e in events.OrderBy(x => x.Sequence))
        {
            switch (e.Data)
            {
                case ILineEvent lineEvent:
                    grouping.AddEvent(lineEvent.LineId, e);
                    break;
                case TripCreated:
                    this.GroupTripCreated(session, (IEvent<TripCreated>)e, grouping, lineIdsByTrip);
                    break;
                case SegmentCreated:
                    this.GroupSegmentCreated(session, (IEvent<SegmentCreated>)e, grouping, lineIdsBySegment);
                    break;
                case StationLocationSet:
                    this.GroupLocationSet(session, (IEvent<StationLocationSet>)e, grouping);
                    break;
                case SegmentCourseSet:
                    this.GroupCourseSet(session, (IEvent<SegmentCourseSet>)e, grouping);
                    break;
                case SettingsNamingPolicySet:
                    this.GroupNamingPolicySet(session, (IEvent<SettingsNamingPolicySet>)e, grouping, lineIds);
                    break;
            }
        }
    }

    private void GroupTripCreated(IQuerySession session, IEvent<TripCreated> tripCreatedEvent, IEventGrouping<Guid> grouping, Dictionary<Guid, List<Guid>> lineIdsByTrip)
    {
        if (lineIdsByTrip.TryGetValue(tripCreatedEvent.Data.TripId, out var lineIds))
        {
            foreach (var lineId in lineIds)
            {
                grouping.AddEvent(lineId, tripCreatedEvent);
            }
        }
    }

    private void GroupSegmentCreated(IQuerySession session, IEvent<SegmentCreated> segmentCreatedEvent, IEventGrouping<Guid> grouping, Dictionary<Guid, List<Guid>> lineIdsBySegment)
    {
        if (lineIdsBySegment.TryGetValue(segmentCreatedEvent.Data.SegmentId, out var lineIds))
        {
            foreach (var lineId in lineIds)
            {
                grouping.AddEvent(lineId, segmentCreatedEvent);
            }
        }
    }

    private void GroupLocationSet(IQuerySession session, IEvent<StationLocationSet> stationLocationSetEvent, IEventGrouping<Guid> grouping)
    {
        throw new NotImplementedException();
    }

    private void GroupCourseSet(IQuerySession session, IEvent<SegmentCourseSet> segmentCourseSetEvent, IEventGrouping<Guid> grouping)
    {
        throw new NotImplementedException();
    }

    private void GroupNamingPolicySet(IQuerySession session, IEvent<SettingsNamingPolicySet> namingPolicySetEvent, IEventGrouping<Guid> grouping, IEnumerable<Guid> lineIds)
    {
        foreach (var lineId in lineIds)
        {
            grouping.AddEvent(lineId, namingPolicySetEvent);
        }
    }
}