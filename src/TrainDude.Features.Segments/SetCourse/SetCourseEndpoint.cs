// <copyright file="SetCourseEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.SetCourse;

using System.Linq;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Generic;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Shared;
using TrainDude.Features.Stations.GetStation;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class SetCourseEndpoint
{
    public const string Route = "/segment/course/set";

    [AggregateHandler]
    [WolverinePost(Route)]
    public static (UpdatedResponse, Events) Post(SetCourseCommand command, SegmentAggregate aggregate, IMessageBus mediator)
    {
        var domainEvent = aggregate.SetCourse(command.Course);

        var response = new UpdatedResponse(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}