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
using TrainDude.Features.Trips.Contracts.GetTrip;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateTripEndpoint
{
    [WolverinePost(CreateTripCommand.TypeRoute)]
    [Tags("Trips")]
    public static (IResult, IStartStream) Handle(CreateTripCommand command, ClaimsPrincipal user)
    {
        var tripId = Guid.NewGuid();
        var domainEvent = TripAggregate.Make(tripId, user.GetSubject(), command.Number);

        var startStream = MartenOps.StartStream<TripAggregate>(domainEvent.TripId, domainEvent);

        var response = new CreatedResponse(tripId);
        var result = Results.Created(GetTripQuery.Route.Replace("{id}", domainEvent.TripId.ToString()), response);

        return (result, startStream);
    }
}