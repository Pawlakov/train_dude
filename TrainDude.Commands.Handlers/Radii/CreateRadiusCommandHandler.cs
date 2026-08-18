// <copyright file="CreateRadiusCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Radii;

using System;

using TrainDude.Commands.Requests.Radii;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Radii;

using Wolverine;
using Wolverine.Marten;

public class CreateRadiusCommandHandler
{
    public static void Validate(CreateRadiusCommand command)
    {
        if (command.Speed < 1)
        {
            throw new InvalidOperationException("A valid speed is required.");
        }

        if (command.Minimum < 1)
        {
            throw new InvalidOperationException("A valid minimum radius is required.");
        }
    }

    public static (IStartStream, OutgoingMessages) Handle(CreateRadiusCommand command)
    {
        var domainEvent = Radius.Make(command.Id, command.Speed, command.Minimum);

        var startStream = MartenOps.StartStream<Radius>(domainEvent.Id, domainEvent);

        var integrationEvent = new RadiusCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.Speed, domainEvent.Minimum);

        return (startStream, new OutgoingMessages { integrationEvent });
    }
}