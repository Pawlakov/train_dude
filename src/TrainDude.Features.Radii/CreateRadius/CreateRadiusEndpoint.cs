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
        var domainEvent = RadiusAggregate.Make(command.RadiusId, user.GetSubject(), command.Speed, command.Minimum);

        var startStream = MartenOps.StartStream<RadiusAggregate>(domainEvent.Id, domainEvent);

        var getUrl = string.Format(GetRadiusQuery.Route, domainEvent.Id);
        var response = new CreationResponse(getUrl);
        var result = Results.Created(getUrl, response);

        return (result, startStream);
    }
}