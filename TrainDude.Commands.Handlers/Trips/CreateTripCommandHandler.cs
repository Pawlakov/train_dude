// <copyright file="CreateTripCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Trips;

using System;

using Marten;

using TrainDude.Commands.Requests.Trips;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Trips;

using Wolverine;

public static class CreateTripCommandHandler
{
    public static OutgoingMessages Handle(CreateTripCommand command, IDocumentSession session)
    {
        var created = Trip.Make(command.Id, command.Number);

        session.Events.StartStream<Trip>(command.Id, created);

        var integrationEvent = new TripCreatedIntegrationEvent(command.Id, 1L, command.Number);

        return new OutgoingMessages { integrationEvent };
    }
}