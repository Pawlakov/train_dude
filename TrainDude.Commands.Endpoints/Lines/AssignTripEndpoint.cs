// <copyright file="AssignTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System.Threading.Tasks;

using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public class AssignTripEndpoint
{
    [AggregateHandler]
    [WolverinePost(AssignTripCommand.Route)]
    public static Task<(UpdatedResponse, Events, OutgoingMessages)> Post(AssignTripCommand command, Line aggregate, [ReadModel(nameof(AssignTripCommand.TripId))] Trip trip)
    {
        var domainEvent = aggregate.AssignTrip(trip.Id);

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new LineTripAssignedIntegrationEvent(domainEvent.Id, aggregate.Version + 1, new(trip.Id, trip.TripNumber));

        return Task.FromResult((response, new Events { domainEvent }, new OutgoingMessages { integrationEvent }));
    }
}