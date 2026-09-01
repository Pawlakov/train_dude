// <copyright file="AssignTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AssignTrip;

using TrainDude.Features.Generic;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AssignTripEndpoint
{
    public const string Route = "/line/trips/assign";

    [AggregateHandler]
    [WolverinePost(Route)]
    public static (UpdatedResponse, Events) Post(AssignTripCommand command, LineAggregate aggregate, [ReadModel(nameof(AssignTripCommand.TripId))] LineTripReference tripAggregate)
    {
        var domainEvent = aggregate.AssignTrip(tripAggregate.Id);

        var response = new UpdatedResponse(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}