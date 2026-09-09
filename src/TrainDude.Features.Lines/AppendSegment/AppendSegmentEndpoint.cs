// <copyright file="AppendSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AppendSegment;

using System;

using Marten;

using TrainDude.Features.Lines.Contracts.AppendSegment;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AppendSegmentEndpoint
{
    [AggregateHandler]
    [WolverinePost(AppendSegmentCommand.TypeRoute)]
    public static (UpdatedResult, Events) Post(AppendSegmentCommand command, LineAggregate aggregate, [ReadModel(nameof(AppendSegmentCommand.SegmentId))] LineSegmentReference segmentAggregate, IQuerySession session)
    {
        var domainEvent = aggregate.AppendSegment(segmentAggregate);

        var response = new UpdatedResult(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}