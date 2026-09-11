// <copyright file="CreateSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.CreateSegment;

using System.Security.Claims;
using System.Threading;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.CreateSegment;
using TrainDude.Features.Segments.Contracts.GetSegment;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.Domain.Exceptions;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Segments.ReadModels;
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

        var domainEvent = SegmentAggregate.Make(command.SegmentId, user.GetSubject(), command.NominalLength, command.Tracks, aEnd, bEnd);

        var startStream = MartenOps.StartStream<SegmentAggregate>(domainEvent.Id, domainEvent);

        var getUrl = GetSegmentQuery.Route.Replace("{id}", domainEvent.Id.ToString());
        var response = new CreationResponse(getUrl);
        var result = Results.Created(getUrl, response);

        return (result, startStream);
    }
}