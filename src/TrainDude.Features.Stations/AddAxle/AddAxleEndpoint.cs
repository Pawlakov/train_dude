// <copyright file="AddAxleEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.AddAxle;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AddAxleEndpoint
{
    [WolverinePost(AddAxleCommand.TypeRoute)]
    [Tags("Stations")]
    public static (IResult, Events) Handle(AddAxleCommand command, [WriteModel(FromRoute = "0", VersionSource = "version")] StationAggregate aggregate, ClaimsPrincipal user)
    {
        var domainEvent = aggregate.AddAxle(user.GetSubject());

        var result = Results.Ok();

        return (result, new Events { domainEvent });
    }
}