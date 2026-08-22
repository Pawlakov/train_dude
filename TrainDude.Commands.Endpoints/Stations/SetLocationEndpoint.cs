// <copyright file="SetLocationEndpoint.cs" company="Pawlakov">
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

public static class SetLocationEndpoint
{
    [AggregateHandler]
    [WolverinePost(SetLocationCommand.Route)]
    public static Task<(UpdatedResponse, Events, OutgoingMessages)> Post(SetLocationCommand command, Station aggregate)
    {
        var domainEvent = aggregate.SetLocation(command.Location);

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new StationLocationSetIntegrationEvent(command.Id, aggregate.Version + 1, command.Location);

        return Task.FromResult((response, new Events { domainEvent }, new OutgoingMessages { integrationEvent }));
    }
}