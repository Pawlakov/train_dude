// <copyright file="AppendSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AppendSegment;

using System;

using Marten;

using TrainDude.Features.Generic;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AppendSegmentEndpoint
{
    public const string Route = "/line/segments/append";

    [AggregateHandler]
    [WolverinePost(Route)]
    public static (UpdatedResponse, Events) Post(AppendSegmentCommand command, LineAggregate aggregate, [ReadModel(nameof(AppendSegmentCommand.SegmentId))] LineSegmentReference segmentAggregate, IQuerySession session)
    {
        var events = new Events();

        var appendEvent = aggregate.AppendSegment(segmentAggregate.Id);
        events.Add(appendEvent);
        aggregate.Apply(appendEvent);

        var response = new UpdatedResponse(aggregate.Version + 1);

        return (response, events);
    }
}