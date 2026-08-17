// <copyright file="CreateTripCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Trips;

using System;

using TrainDude.Commands.Requests.Trips;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Trips;

using Wolverine;
using Wolverine.Marten;

public static class CreateTripCommandHandler
{
    public static void Validate(CreateTripCommand command)
    {
        if (command.Number == default)
        {
            throw new InvalidOperationException("A valid trip number is required.");
        }
    }

    public static (IStartStream, OutgoingMessages) Handle(CreateTripCommand command)
    {
        var domainEvent = Trip.Make(command.Id, command.Number);

        var startStream = MartenOps.StartStream<Trip>(command.Id, domainEvent);

        var integrationEvent = new TripCreatedIntegrationEvent(command.Id, 1L, command.Number);

        return (startStream, new OutgoingMessages { integrationEvent });
    }
}