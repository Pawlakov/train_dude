// <copyright file="AddAxleCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Stations;

using TrainDude.Commands.Requests.Stations;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Stations;

using Wolverine;
using Wolverine.Marten;

public static class AddAxleCommandHandler
{
    [AggregateHandler]
    public static (Events, OutgoingMessages) Handle(AddAxleCommand command, Station aggregate)
    {
        var domainEvent = aggregate.AddAxle();

        var integrationEvent = new StationAxleAddedIntegrationEvent(command.Id, aggregate.Version + 1);

        return (new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}