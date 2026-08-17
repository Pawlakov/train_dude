// <copyright file="SetLocationCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Stations;

using System;

using TrainDude.Commands.Requests.Stations;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Stations;

using Wolverine;
using Wolverine.Marten;

public static class SetLocationCommandHandler
{
    public static void Validate(SetLocationCommand command)
    {
        if (command.Location == default)
        {
            throw new InvalidOperationException("A valid location is required.");
        }
    }

    [AggregateHandler]
    public static (Events, OutgoingMessages) Handle(SetLocationCommand command, Station aggregate)
    {
        var domainEvent = aggregate.SetLocation(command.Location);

        var integrationEvent = new StationLocationSetIntegrationEvent(command.Id, aggregate.Version + 1, command.Location);

        return (new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}