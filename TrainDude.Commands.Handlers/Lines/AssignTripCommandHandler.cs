// <copyright file="AssignTripCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Lines;

using System;

using JasperFx.Events;

using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AssignTripCommandHandler
{
    public static void Validate(AssignTripCommand command)
    {
        if (command.TripId == Guid.Empty)
        {
            throw new InvalidOperationException("A valid trip ID is required.");
        }
    }

    [AggregateHandler]
    public static (Events, OutgoingMessages) Handle(AssignTripCommand command, Line aggregate, [ReadModel(nameof(AssignTripCommand.TripId))] Trip trip)
    {
        var domainEvent = aggregate.AssignTrip(trip.Id);

        var integrationEvent = new LineTripAssignedIntegrationEvent(domainEvent.Id, aggregate.Version + 1, new(trip.Id, trip.TripNumber));

        return (new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}