// <copyright file="AssignTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System.Threading.Tasks;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Lines;
using TrainDude.Domain.Lines;
using TrainDude.Domain.Trips;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AssignTripEndpoint
{
    [AggregateHandler]
    [WolverinePost(AssignTripCommand.Route)]
    public static Task<(UpdatedResponse, Events, OutgoingMessages)> Post(AssignTripCommand command, LineAggregate aggregate, [ReadModel(nameof(AssignTripCommand.TripId))] TripAggregate tripAggregate)
    {
        var domainEvent = aggregate.AssignTrip(tripAggregate.Id);

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new LineTripAssignedIntegrationEvent(domainEvent.Id, aggregate.Version + 1, new(tripAggregate.Id, tripAggregate.TripNumber));

        return Task.FromResult((response, new Events { domainEvent }, new OutgoingMessages { integrationEvent }));
    }
}