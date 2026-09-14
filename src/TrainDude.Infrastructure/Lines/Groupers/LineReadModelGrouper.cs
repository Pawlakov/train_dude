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

        foreach (var e in events.OrderBy(x => x.Sequence))
        {
            if (e.Data is ILineEvent lineEvent)
            {
                grouping.AddEvent(lineEvent.LineId, e);
            }
            else if (e.Data is TripCreated)
            {
                this.GroupTripCreated(session, (IEvent<TripCreated>)e, grouping, lineIdsByTrip);
            }
            else if (e.Data is SegmentCreated)
            {
                this.GroupSegmentCreated(session, (IEvent<SegmentCreated>)e, grouping, lineIdsBySegment);
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
}