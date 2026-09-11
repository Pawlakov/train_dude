// <copyright file="CreateStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.CreateStation;

using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.Contracts.GetStation;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateStationEndpoint
{
    [WolverinePost(CreateStationCommand.TypeRoute)]
    [Tags("Stations")]
    public static (IResult, IStartStream) Handle(CreateStationCommand command, ClaimsPrincipal user)
    {
        var domainEvent = StationAggregate.Make(command.StationId, user.GetSubject(), command.NameGerman, command.NameGermanNew, command.NamePolish, command.NameRussian);

        var startStream = MartenOps.StartStream<StationAggregate>(domainEvent.Id, domainEvent);

        var getUrl = GetStationQuery.Route.Replace("{id}", domainEvent.Id.ToString());
        var response = new CreationResponse(getUrl);
        var result = Results.Created(getUrl, response);

        return (result, startStream);
    }
}