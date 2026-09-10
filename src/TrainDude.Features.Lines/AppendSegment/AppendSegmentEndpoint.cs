// <copyright file="AppendSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AppendSegment;

using System;
using System.Security.Claims;

using Marten;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Lines.Contracts.AppendSegment;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AppendSegmentEndpoint
{
    [WolverinePost(AppendSegmentCommand.TypeRoute)]
    [Tags("Lines")]
    public static (IResult, Events) Handle(AppendSegmentCommand command, [WriteModel(FromRoute = "0")] LineAggregate aggregate, ClaimsPrincipal user, [ReadModel(nameof(AppendSegmentCommand.SegmentId))] LineSegmentReference segmentAggregate, IQuerySession session)
    {
        var domainEvent = aggregate.AppendSegment(user.GetSubject(), segmentAggregate);

        var result = Results.Ok();

        return (result, new Events { domainEvent });
    }
}