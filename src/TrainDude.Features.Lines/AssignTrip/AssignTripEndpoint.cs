// <copyright file="AssignTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AssignTrip;

using TrainDude.Features.Lines.Contracts.AssignTrip;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AssignTripEndpoint
{
    [AggregateHandler]
    [WolverinePost(AssignTripCommand.TypeRoute)]
    public static (UpdatedResult, Events) Post(AssignTripCommand command, LineAggregate aggregate, [ReadModel(nameof(AssignTripCommand.TripId))] LineTripReference tripAggregate)
    {
        var domainEvent = aggregate.AssignTrip(tripAggregate.Id);

        var response = new UpdatedResult(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}