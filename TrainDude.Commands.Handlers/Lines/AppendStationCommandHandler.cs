// <copyright file="AppendStationCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Lines;

using System;

using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Marten;

public static class AppendStationCommandHandler
{
    public static void Validate(AppendStationCommand command)
    {
        if (command.StationId == Guid.Empty)
        {
            throw new InvalidOperationException("A valid station is required.");
        }
    }

    [AggregateHandler]
    public static (Events, OutgoingMessages) Handle(AppendStationCommand command, Line aggregate)
    {
        var domainEvent = aggregate.AppendStation(command.StationId);

        var integrationEvent = new LineStationAppendedIntegrationEvent(command.Id, aggregate.Version + 1, command.StationId);

        return (new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}