// <copyright file="AddAxleEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.AddAxle;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AddAxleEndpoint
{
    public static async Task<ProblemDetails> Before(AddAxleCommand command, StationAggregate aggregate)
    {
        if (aggregate.Version != command.ExpectedVersion)
        {
            return new ProblemDetails
            {
                Status = 409,
                Title = "Stale version",
                Detail = $"Expected version {command.ExpectedVersion}, but the stream is now at version {aggregate.Version}.",
            };
        }

        return WolverineContinue.NoProblems;
    }

    [WolverinePost(AddAxleCommand.TypeRoute)]
    [Tags("Stations")]
    public static (IResult, Events) Handle(AddAxleCommand command, [WriteModel(FromRoute = "id")] StationAggregate aggregate, ClaimsPrincipal user)
    {
        var domainEvent = aggregate.AddAxle(user.GetSubject());

        var result = Results.Ok();

        return (result, new Events { domainEvent });
    }
}