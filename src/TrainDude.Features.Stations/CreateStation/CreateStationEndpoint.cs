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
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateStationEndpoint
{
    [WolverinePost(CreateStationCommand.TypeRoute)]
    [Tags("Stations")]
    public static (CreatedResult, IStartStream) Handle(CreateStationCommand command, ClaimsPrincipal user)
    {
        var id = Guid.NewGuid();
        var domainEvent = StationAggregate.Make(id, user.GetSubject(), command.NameGerman, command.NameGermanNew, command.NamePolish, command.NameRussian);

        var startStream = MartenOps.StartStream<StationAggregate>(id, domainEvent);

        var response = new CreatedResult(id);

        return (response, startStream);
    }
}