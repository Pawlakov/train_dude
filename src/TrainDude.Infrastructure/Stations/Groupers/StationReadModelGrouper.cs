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
        foreach (var e in events)
        {
            if (e is IEvent<SettingsNamingPolicySet> namingPolicySetEvent)
            {
                await this.GroupNamingPolicySet(session, namingPolicySetEvent, grouping);
            }
            else if (e is IEvent<IStationEvent> stationEvent)
            {
                grouping.AddEvent(stationEvent.Data.StationId, stationEvent);
            }
        }
    }

    private async Task GroupNamingPolicySet(IQuerySession session, IEvent<SettingsNamingPolicySet> namingPolicySetEvent, IEventGrouping<Guid> grouping)
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