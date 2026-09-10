// <copyright file="CreateRadiusEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.CreateRadius;

using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Radii.Contracts.CreateRadius;
using TrainDude.Features.Radii.Domain;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateRadiusEndpoint
{
    [WolverinePost(CreateRadiusCommand.TypeRoute)]
    [Tags("Radii")]
    public static (CreatedResult, IStartStream) Handle(CreateRadiusCommand command, ClaimsPrincipal user)
    {
        var id = Guid.NewGuid();
        var domainEvent = RadiusAggregate.Make(id, user.GetSubject(), command.Speed, command.Minimum);

        var startStream = MartenOps.StartStream<RadiusAggregate>(id, domainEvent);

        var response = new CreatedResult(domainEvent.Id);

        return (response, startStream);
    }
}