// <copyright file="StationReadModelGrouper.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Infrastructure.Stations.Groupers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JasperFx.Events;
using JasperFx.Events.Grouping;

using Marten;
using Marten.Events.Aggregation;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Stations.Domain;

public class StationReadModelGrouper
    : IAggregateGrouper<Guid>
{
    public async Task Group(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        var policyEvents = events.OfType<IEvent<SettingsNamingPolicySet>>().ToList();
        if (policyEvents.Count == 0)
        {
            return;
        }

        var allSegmentIds = await session.Query<StationAggregate>()
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