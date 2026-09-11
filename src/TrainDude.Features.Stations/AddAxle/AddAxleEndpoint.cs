// <copyright file="AddAxleEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.AddAxle;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Http.Marten;
using Wolverine.Marten;

public static class AddAxleEndpoint
{
    [WolverinePost(AddAxleCommand.TypeRoute)]
    [Tags("Stations")]
    public static (IResult, Events) Handle(AddAxleCommand command, [Document(FromRoute = "id")] StationAggregate aggregate, ClaimsPrincipal user)
    {
        var domainEvent = aggregate.AddAxle(user.GetSubject());

        var result = Results.Ok();

        return (result, new Events { domainEvent });
    }
}