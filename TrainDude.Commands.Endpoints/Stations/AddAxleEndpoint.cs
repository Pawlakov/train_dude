// <copyright file="AddAxleEndpoint.cs" company="Pawlakov">
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

public static class AddAxleEndpoint
{
    [AggregateHandler]
    [WolverinePost(AddAxleCommand.Route)]
    public static Task<(UpdatedResponse, Events, OutgoingMessages)> Post(AddAxleCommand command, StationAggregate aggregate)
    {
        var domainEvent = aggregate.AddAxle();

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new StationAxleAddedIntegrationEvent(domainEvent.Id, aggregate.Version + 1);

        return Task.FromResult((response, new Events { domainEvent }, new OutgoingMessages { integrationEvent }));
    }
}