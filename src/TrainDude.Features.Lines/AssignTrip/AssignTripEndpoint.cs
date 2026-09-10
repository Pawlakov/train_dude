// <copyright file="AssignTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AssignTrip;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Lines.Contracts.AssignTrip;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AssignTripEndpoint
{
    [WolverinePost(AssignTripCommand.TypeRoute)]
    [Tags("Lines")]
    public static (UpdatedResult, Events) Handle(AssignTripCommand command, [WriteModel(FromRoute = "id", VersionSource = "version")] LineAggregate aggregate, ClaimsPrincipal user, [ReadModel(nameof(AssignTripCommand.TripId))] LineTripReference tripAggregate)
    {
        var domainEvent = aggregate.AssignTrip(user.GetSubject(), tripAggregate);

        var response = new UpdatedResult(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}