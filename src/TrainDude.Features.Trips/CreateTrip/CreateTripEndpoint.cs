// <copyright file="CreateTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.CreateTrip;

using System;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Trips.Contracts.CreateTrip;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateTripEndpoint
{
    [WolverinePost(CreateTripCommand.Route)]
    public static (CreatedResult, IStartStream) Post(CreateTripCommand tripCommand)
    {
        var id = Guid.NewGuid();
        var domainEvent = TripAggregate.Make(id, tripCommand.Number);

        var startStream = MartenOps.StartStream<TripAggregate>(id, domainEvent);

        var response = new CreatedResult(id);

        return (response, startStream);
    }
}