// <copyright file="CreateRadiusEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.CreateRadius;

using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Radii.Contracts.CreateRadius;
using TrainDude.Features.Radii.Contracts.GetRadius;
using TrainDude.Features.Radii.Domain;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateRadiusEndpoint
{
    [WolverinePost(CreateRadiusCommand.TypeRoute)]
    [Tags("Radii")]
    public static (IResult, IStartStream) Handle(CreateRadiusCommand command, ClaimsPrincipal user)
    {
        var radiusId = Guid.NewGuid();
        var domainEvent = RadiusAggregate.Make(radiusId, user.GetSubject(), command.Speed, command.Minimum);

        var startStream = MartenOps.StartStream<RadiusAggregate>(domainEvent.RadiusId, domainEvent);

        var response = new CreatedResponse(radiusId);
        var result = Results.Created(GetRadiusQuery.Route.Replace("{id}", domainEvent.RadiusId.ToString()), response);

        return (result, startStream);
    }
}