// <copyright file="CreateSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.CreateSegment;

using System;
using System.Security.Claims;
using System.Threading;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.CreateSegment;
using TrainDude.Features.Segments.Contracts.GetSegment;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.Domain.Exceptions;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class CreateSegmentEndpoint
{
    [WolverinePost(CreateSegmentCommand.TypeRoute)]
    [Tags("Segments")]
    public static (IResult, IStartStream) Handle(CreateSegmentCommand command, ClaimsPrincipal user, [ReadModel(nameof(CreateSegmentCommand.AId))] SegmentStationReference a, [ReadModel(nameof(CreateSegmentCommand.BId))] SegmentStationReference b, CancellationToken cancellationToken = default)
    {
        var aEnd = new SegmentEnd(command.AId, command.AAxle, command.APole);
        var bEnd = new SegmentEnd(command.BId, command.BAxle, command.BPole);
        if (command.AAxle >= a.AxleCount)
        {
            throw new SegmentNoStationAxleException(command.AAxle, command.AId, a.AxleCount);
        }

        if (command.BAxle >= b.AxleCount)
        {
            throw new SegmentNoStationAxleException(command.BAxle, command.BId, b.AxleCount);
        }

        var segmentId = Guid.NewGuid();
        var domainEvent = SegmentAggregate.Make(segmentId, user.GetSubject(), command.NominalLength, command.Tracks, aEnd, bEnd);

        var startStream = MartenOps.StartStream<SegmentAggregate>(domainEvent.SegmentId, domainEvent);

        var response = new CreatedResponse(segmentId);
        var result = Results.Created(GetSegmentQuery.Route.Replace("{id}", domainEvent.SegmentId.ToString()), response);

        return (result, startStream);
    }
}