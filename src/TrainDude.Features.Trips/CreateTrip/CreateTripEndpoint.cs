// <copyright file="CreateTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.CreateTrip;

using System;

using TrainDude.Features.Generic;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateTripEndpoint
{
    public const string Route = "/trip/create";

    [WolverinePost(Route)]
    public static (CreatedResponse, IStartStream) Post(CreateTripCommand tripCommand)
    {
        var id = Guid.NewGuid();
        var domainEvent = TripAggregate.Make(id, tripCommand.Number);

        var startStream = MartenOps.StartStream<TripAggregate>(id, domainEvent);

        var response = new CreatedResponse(id);

        return (response, startStream);
    }
}