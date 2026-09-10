// <copyright file="SetCourseEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.SetCourse;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.SetCourse;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class SetCourseEndpoint
{
    [WolverinePost(SetCourseCommand.TypeRoute)]
    [Tags("Segments")]
    public static (IResult, Events) Handle(SetCourseCommand command, ClaimsPrincipal user, [WriteModel(FromRoute = "0", VersionSource = "version")] SegmentAggregate aggregate)
    {
        var domainEvent = aggregate.SetCourse(user.GetSubject(), command.Course);

        var result = Results.Ok();

        return (result, new Events { domainEvent });
    }
}