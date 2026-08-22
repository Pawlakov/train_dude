// <copyright file="LineDuplicateStationException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Exceptions;

using System;

using Microsoft.AspNetCore.Http;

public sealed class LineDuplicateStationException
    : DomainException
{
    public Guid LineId { get; }

    public Guid StationId { get; }

    public LineDuplicateStationException(Guid lineId, Guid stationId)
        : base($"This station ({stationId}) is already on this line ({lineId}).")
    {
        this.LineId = lineId;
        this.StationId = stationId;
    }

    public override int StatusCode => StatusCodes.Status409Conflict;
}