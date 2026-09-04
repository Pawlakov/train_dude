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
        var a = await mediator.InvokeAsync<GetStationQueryResult>(new GetStationQuery(segmentCommand.A.Id), cancellationToken);
        var b = await mediator.InvokeAsync<GetStationQueryResult>(new GetStationQuery(segmentCommand.B.Id), cancellationToken);

        var aEnd = new SegmentEnd(segmentCommand.A.Id, segmentCommand.A.Axle, segmentCommand.A.Pole);
        var bEnd = new SegmentEnd(segmentCommand.B.Id, segmentCommand.B.Axle, segmentCommand.B.Pole);
        if (segmentCommand.A.Axle >= a.AxleCount)
        {
            throw new SegmentNoStationAxleException(segmentCommand.A.Axle, segmentCommand.A.Id, a.AxleCount);
        }

        if (segmentCommand.B.Axle >= b.AxleCount)
        {
            throw new SegmentNoStationAxleException(segmentCommand.B.Axle, segmentCommand.B.Id, b.AxleCount);
        }

        var id = Guid.NewGuid();
        var domainEvent = SegmentAggregate.Make(id, segmentCommand.NominalLength, segmentCommand.Tracks, aEnd, bEnd);

        var startStream = MartenOps.StartStream<SegmentAggregate>(domainEvent.Id, domainEvent);

        var response = new CreatedResult(domainEvent.Id);

        return (response, startStream);
    }
}