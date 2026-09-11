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
        var domainEvent = TripAggregate.Make(command.TripId, user.GetSubject(), command.Number);

        var startStream = MartenOps.StartStream<TripAggregate>(domainEvent.Id, domainEvent);

        var getUrl = GetTripQuery.Route.Replace("{id}", domainEvent.Id.ToString());
        var response = new CreationResponse(getUrl);
        var result = Results.Created(getUrl, response);

        return (result, startStream);
    }
}