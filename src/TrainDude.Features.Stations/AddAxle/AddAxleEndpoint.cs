// <copyright file="AddAxleEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using System.Security.Claims;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.AddAxle;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class AddAxleEndpoint
{
    [AggregateHandler]
    [WolverinePost(AddAxleCommand.TypeRoute)]
    public static (UpdatedResult, Events) Handle(AddAxleCommand command, ClaimsPrincipal user, StationAggregate aggregate)
    {
        var domainEvent = aggregate.AddAxle(user.GetSubject());

        var response = new UpdatedResult(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}