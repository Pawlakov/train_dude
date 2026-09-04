// <copyright file="SetCourseEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.SetCourse;

using TrainDude.Features.Segments.Contracts.SetCourse;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class SetCourseEndpoint
{
    [AggregateHandler]
    [WolverinePost(SetCourseCommand.TypeRoute)]
    public static (UpdatedResult, Events) Post(SetCourseCommand command, SegmentAggregate aggregate, IMessageBus mediator)
    {
        var domainEvent = aggregate.SetCourse(command.Course);

        var response = new UpdatedResult(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}