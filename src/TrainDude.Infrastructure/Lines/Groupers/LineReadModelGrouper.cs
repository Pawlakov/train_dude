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
using TrainDude.Features.Segments.Domain;
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
            .Select(x => new { x.Id, x.Trips, x.Segments })
            .ToListAsync();

        var segmentLinks = await session.Query<SegmentAggregate>()
            .Select(x => new { x.Id, x.A, x.B })
            .ToListAsync();

        var stationIdsBySegment = segmentLinks.ToDictionary(x => x.Id, x => new List<Guid> { x.A.Id, x.B.Id });

        var lineIdsByTrip = links
            .SelectMany(x => x.Trips.Select(y => new { TripId = y, LineId = x.Id }))
            .GroupBy(x => x.TripId)
            .ToDictionary(x => x.Key, x => x.Select(y => y.LineId).ToList());

        var lineIdsBySegment = links
            .SelectMany(x => x.Segments.Select(y => new { SegmentId = y, LineId = x.Id }))
            .GroupBy(x => x.SegmentId)
            .ToDictionary(x => x.Key, x => x.Select(y => y.LineId).ToList());

        var lineIdsByStation = links
            .SelectMany(x => x.Segments.Select(y => new { Stations = stationIdsBySegment[y], LineId = x.Id }))
            .SelectMany(x => x.Stations.Select(y => new { StationId = y, x.LineId }))
            .GroupBy(x => x.StationId)
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
                    this.GroupTripCreated((IEvent<TripCreated>)e, grouping, lineIdsByTrip);
                    break;
                case SegmentCreated:
                    this.GroupSegmentCreated((IEvent<SegmentCreated>)e, grouping, lineIdsBySegment);
                    break;
                case StationLocationSet:
                    this.GroupLocationSet((IEvent<StationLocationSet>)e, grouping, lineIdsByStation);
                    break;
                case SegmentCourseSet:
                    this.GroupCourseSet((IEvent<SegmentCourseSet>)e, grouping, lineIdsBySegment);
                    break;
                case SettingsNamingPolicySet:
                    this.GroupNamingPolicySet((IEvent<SettingsNamingPolicySet>)e, grouping, lineIds);
                    break;
            }
        }
    }

    private void GroupTripCreated(IEvent<TripCreated> tripCreatedEvent, IEventGrouping<Guid> grouping, Dictionary<Guid, List<Guid>> lineIdsByTrip)
    {
        if (lineIdsByTrip.TryGetValue(tripCreatedEvent.Data.TripId, out var lineIds))
        {
            foreach (var lineId in lineIds)
            {
                grouping.AddEvent(lineId, tripCreatedEvent);
            }
        }
    }

    private void GroupSegmentCreated(IEvent<SegmentCreated> segmentCreatedEvent, IEventGrouping<Guid> grouping, Dictionary<Guid, List<Guid>> lineIdsBySegment)
    {
        if (lineIdsBySegment.TryGetValue(segmentCreatedEvent.Data.SegmentId, out var lineIds))
        {
            foreach (var lineId in lineIds)
            {
                grouping.AddEvent(lineId, segmentCreatedEvent);
            }
        }
    }

    private void GroupLocationSet(IEvent<StationLocationSet> stationLocationSetEvent, IEventGrouping<Guid> grouping, Dictionary<Guid, List<Guid>> lineIdsByStation)
    {
        if (lineIdsByStation.TryGetValue(stationLocationSetEvent.Data.StationId, out var lineIds))
        {
            foreach (var lineId in lineIds)
            {
                grouping.AddEvent(lineId, stationLocationSetEvent);
            }
        }
    }

    private void GroupCourseSet(IEvent<SegmentCourseSet> segmentCourseSetEvent, IEventGrouping<Guid> grouping, Dictionary<Guid, List<Guid>> lineIdsBySegment)
    {
        if (lineIdsBySegment.TryGetValue(segmentCourseSetEvent.Data.SegmentId, out var lineIds))
        {
            foreach (var lineId in lineIds)
            {
                grouping.AddEvent(lineId, segmentCourseSetEvent);
            }
        }
    }

    private void GroupNamingPolicySet(IEvent<SettingsNamingPolicySet> namingPolicySetEvent, IEventGrouping<Guid> grouping, IEnumerable<Guid> lineIds)
    {
        foreach (var lineId in lineIds)
        {
            grouping.AddEvent(lineId, namingPolicySetEvent);
        }
    }
}