// <copyright file="CreateStationCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Stations;

using System;

using TrainDude.Commands.Requests.Stations;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Stations;

using Wolverine;
using Wolverine.Marten;

public static class CreateStationCommandHandler
{
    public static void Validate(CreateStationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.NameGerman))
        {
            throw new InvalidOperationException("A valid name is required.");
        }
    }

    public static (IStartStream, OutgoingMessages) Handle(CreateStationCommand command)
    {
        var domainEvent = Station.Make(command.Id, command.NameGerman, command.NameGermanNew, command.NamePolish, command.NameRussian);

        var startStream = MartenOps.StartStream<Station>(command.Id, domainEvent);

        var integrationEvent = new StationCreatedIntegrationEvent(command.Id, 1L, command.NameGerman, command.NameGermanNew, command.NamePolish, command.NameRussian);

        return (startStream, new OutgoingMessages { integrationEvent });
    }
}