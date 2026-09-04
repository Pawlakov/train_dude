// <copyright file="CreateSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.CreateSegment;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Segments.Contracts.CreateSegment;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.Domain.Exceptions;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.GetStation;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateSegmentEndpoint
{
    [WolverinePost(CreateSegmentCommand.TypeRoute)]
    public static async Task<(CreatedResult, IStartStream)> Post(CreateSegmentCommand segmentCommand, IMessageBus mediator, CancellationToken cancellationToken = default)
    {
        var a = await mediator.InvokeAsync<GetStationQueryResult>(new GetStationQuery(segmentCommand.AId), cancellationToken);
        var b = await mediator.InvokeAsync<GetStationQueryResult>(new GetStationQuery(segmentCommand.BId), cancellationToken);

        var aEnd = new SegmentEnd(segmentCommand.AId, segmentCommand.AAxle, segmentCommand.APole);
        var bEnd = new SegmentEnd(segmentCommand.BId, segmentCommand.BAxle, segmentCommand.BPole);
        if (segmentCommand.AAxle >= a.AxleCount)
        {
            throw new SegmentNoStationAxleException(segmentCommand.AAxle, segmentCommand.AId, a.AxleCount);
        }

        if (segmentCommand.BAxle >= b.AxleCount)
        {
            throw new SegmentNoStationAxleException(segmentCommand.BAxle, segmentCommand.BId, b.AxleCount);
        }

        var id = Guid.NewGuid();
        var domainEvent = SegmentAggregate.Make(id, segmentCommand.NominalLength, segmentCommand.Tracks, aEnd, bEnd);

        var startStream = MartenOps.StartStream<SegmentAggregate>(domainEvent.Id, domainEvent);

        var response = new CreatedResult(domainEvent.Id);

        return (response, startStream);
    }
}