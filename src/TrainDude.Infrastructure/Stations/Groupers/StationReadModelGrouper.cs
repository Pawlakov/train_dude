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
using TrainDude.Features.Stations.Domain.Events;

public class StationReadModelGrouper
    : IAggregateGrouper<Guid>
{
    public async Task Group(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        await this.GroupNamingPolicySet(session, events, grouping);
    }

    private async Task GroupNamingPolicySet(IQuerySession session, IReadOnlyList<IEvent> events, IEventGrouping<Guid> grouping)
    {
        var namingPolicySetEvents = events.OfType<IEvent<SettingsNamingPolicySet>>().ToList();
        if (namingPolicySetEvents.Count == 0)
        {
            return;
        }

        foreach (var namingPolicySetEvent in namingPolicySetEvents)
        {
            var sequence = namingPolicySetEvent.Sequence;

            var stationIds = await session.Events
                .QueryAllRawEvents()
                .OfType<IEvent<StationCreated>>()
                .Where(x => x.Sequence < sequence)
                .Select(x => x.Data.StationId)
                .Distinct()
                .ToListAsync();

            foreach (var stationId in stationIds)
            {
                grouping.AddEvent(stationId, namingPolicySetEvent);
            }
        }
    }
}