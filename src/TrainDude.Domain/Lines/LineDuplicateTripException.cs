// <copyright file="LineDuplicateTripException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;

using TrainDude.Domain.Base;

public sealed class LineDuplicateTripException
    : DomainException
{
    public Guid LineId { get; }

    public Guid TripId { get; }

    public LineDuplicateTripException(Guid lineId, Guid tripId)
        : base($"This trip ({tripId}) is already associated with this line ({lineId}).")
    {
        this.LineId = lineId;
        this.TripId = tripId;
    }

    public override ErrorKind StatusCode => ErrorKind.Conflict;
}