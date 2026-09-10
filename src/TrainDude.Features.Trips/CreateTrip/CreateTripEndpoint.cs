// <copyright file="CreateTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.CreateTrip;

using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Trips.Contracts.CreateTrip;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateTripEndpoint
{
    [WolverinePost(CreateTripCommand.TypeRoute)]
    [Tags("Trips")]
    public static (CreatedResult, IStartStream) Handle(CreateTripCommand tripCommand, ClaimsPrincipal user)
    {
        var id = Guid.NewGuid();
        var domainEvent = TripAggregate.Make(id, user.GetSubject(), tripCommand.Number);

        var startStream = MartenOps.StartStream<TripAggregate>(id, domainEvent);

        var response = new CreatedResult(id);

        return (response, startStream);
    }
}