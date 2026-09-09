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

using TrainDude.Features.Trips.Domain.Events;
using TrainDude.Infrastructure.Lines.ReadModels;

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

        var links = await session.Query<LineTripLink>()
            .ToListAsync();

        var lineIdsByTrip = links
            .GroupBy(x => x.TripId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Id).ToList());

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