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
using TrainDude.Features.Shared.Contracts.Trips.Domain.Events;

public sealed class LineReadModelGrouper
    : IAggregateGrouper<Guid>
{
    public async Task Group(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        await this.GroupTripCreated(session, events, grouping);
    }

    private async Task GroupTripCreated(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        var tripCreatedEvents = events.OfType<IEvent<TripCreated>>().ToList();
        if (tripCreatedEvents.Count == 0)
        {
            return;
        }

        var links = await session.Query<LineAggregate>()
            .SelectMany(x => x.Trips.Select(y => new { LineId = x.Id, TripId = y }))
            .Distinct()
            .ToListAsync();

        var lineIdsByTrip = links
            .GroupBy(x => x.TripId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.LineId).ToList());

        foreach (var e in tripCreatedEvents)
        {
            var tripId = e.Data.Id;
            if (!lineIdsByTrip.TryGetValue(tripId, out var lineIds))
            {
                continue;
            }

            foreach (var lineId in lineIds)
            {
                grouping.AddEvent(lineId, e);
            }
        }
    }
}