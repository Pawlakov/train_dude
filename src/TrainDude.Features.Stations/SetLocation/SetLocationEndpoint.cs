// <copyright file="SetLocationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.SetLocation;

using System.Security.Claims;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Http.Marten;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class SetLocationEndpoint
{
    public static async Task<ProblemDetails> Before(SetLocationCommand command, StationAggregate aggregate)
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

    [WolverinePost(SetLocationCommand.TypeRoute)]
    [Tags("Stations")]
    public static (IResult, Events) Handle(SetLocationCommand command, [WriteModel(FromRoute = "id")] StationAggregate aggregate, ClaimsPrincipal user)
    {
        var domainEvent = aggregate.SetLocation(user.GetSubject(), command.Location);

        var result = Results.Ok();

        return (result, new Events { domainEvent });
    }
}