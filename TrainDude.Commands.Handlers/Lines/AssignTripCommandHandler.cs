// <copyright file="AssignTripCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Lines;

using System;

using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Marten;

public static class AssignTripCommandHandler
{
    public static void Validate(AssignTripCommand command)
    {
        if (command.TripId == Guid.Empty)
        {
            throw new InvalidOperationException("A valid trip is required.");
        }
    }

    [AggregateHandler]
    public static (Events, OutgoingMessages) Handle(AssignTripCommand command, Line aggregate)
    {
        var domainEvent = aggregate.AssignTrip(command.TripId);

        var integrationEvent = new LineTripAssignedIntegrationEvent(command.Id, aggregate.Version + 1, command.TripId);

        return (new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}