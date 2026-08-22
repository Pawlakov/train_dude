// <copyright file="LineConsecutiveDuplicateStationException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;

using TrainDude.Domain.Base;

public sealed class LineConsecutiveDuplicateStationException
    : DomainException
{
    public Guid LineId { get; }

    public Guid StationId { get; }

    public LineConsecutiveDuplicateStationException(Guid lineId, Guid stationId)
        : base($"This station ({stationId}) is already on this line ({lineId}).")
    {
        this.LineId = lineId;
        this.StationId = stationId;
    }

    public override ErrorKind StatusCode => ErrorKind.Conflict;
}