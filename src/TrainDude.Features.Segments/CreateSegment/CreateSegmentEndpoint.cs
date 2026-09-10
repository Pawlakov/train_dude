// <copyright file="CreateSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.CreateSegment;

using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.CreateSegment;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.Domain.Exceptions;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.GetStation;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class CreateSegmentEndpoint
{
    [WolverinePost(CreateSegmentCommand.TypeRoute)]
    [Tags("Segments")]
    public static async Task<(CreatedResult, IStartStream)> Handle(CreateSegmentCommand segmentCommand, ClaimsPrincipal user, [ReadModel(nameof(CreateSegmentCommand.AId))] SegmentStationReference a, [ReadModel(nameof(CreateSegmentCommand.BId))] SegmentStationReference b, CancellationToken cancellationToken = default)
    {
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
        var domainEvent = SegmentAggregate.Make(id, user.GetSubject(), segmentCommand.NominalLength, segmentCommand.Tracks, aEnd, bEnd);

        var startStream = MartenOps.StartStream<SegmentAggregate>(domainEvent.Id, domainEvent);

        var response = new CreatedResult(domainEvent.Id);

        return (response, startStream);
    }
}