// <copyright file="SetLocationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.SetLocation;

using System.Security.Claims;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class SetLocationEndpoint
{
    [AggregateHandler]
    [WolverinePost(SetLocationCommand.TypeRoute)]
    public static (UpdatedResult, Events) Post(SetLocationCommand command, ClaimsPrincipal user, StationAggregate aggregate)
    {
        var domainEvent = aggregate.SetLocation(user.GetSubject(), command.Location);

        var response = new UpdatedResult(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}