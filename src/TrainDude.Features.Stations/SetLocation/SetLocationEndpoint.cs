// <copyright file="SetLocationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.SetLocation;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class SetLocationEndpoint
{
    [WolverinePost(SetLocationCommand.TypeRoute)]
    [Tags("Stations")]
    public static (IResult, Events) Handle(SetLocationCommand command, [WriteModel(FromRoute = "0", VersionSource = "version")] StationAggregate aggregate, ClaimsPrincipal user)
    {
        var domainEvent = aggregate.SetLocation(user.GetSubject(), command.Location);

        var result = Results.Ok();

        return (result, new Events { domainEvent });
    }
}