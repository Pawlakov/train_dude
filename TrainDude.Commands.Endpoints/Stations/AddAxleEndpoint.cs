// <copyright file="AddAxleEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System.Threading.Tasks;

using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Stations;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Stations;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class AddAxleEndpoint
{
    [AggregateHandler]
    [WolverinePost(AddAxleCommand.Route)]
    public static Task<(UpdatedResponse, Events, OutgoingMessages)> Post(AddAxleCommand command, Station aggregate)
    {
        var domainEvent = aggregate.AddAxle();

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new StationAxleAddedIntegrationEvent(command.Id, aggregate.Version + 1);

        return Task.FromResult((response, new Events { domainEvent }, new OutgoingMessages { integrationEvent }));
    }
}