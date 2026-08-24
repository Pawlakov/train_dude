// <copyright file="SetLocationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System.Threading.Tasks;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Stations;
using TrainDude.Domain.Stations;
using TrainDude.Integration.Events.Stations;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class SetLocationEndpoint
{
    [AggregateHandler]
    [WolverinePost(SetLocationCommand.Route)]
    public static Task<(UpdatedResponse, Events, OutgoingMessages)> Post(SetLocationCommand command, StationAggregate aggregate)
    {
        var domainEvent = aggregate.SetLocation(command.Location);

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new StationLocationSetIntegrationEvent(domainEvent.Id, aggregate.Version + 1, domainEvent.Location);

        return Task.FromResult((response, new Events { domainEvent }, new OutgoingMessages { integrationEvent }));
    }
}