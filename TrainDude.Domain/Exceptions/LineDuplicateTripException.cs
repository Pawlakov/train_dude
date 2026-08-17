// <copyright file="LineDuplicateTripException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Exceptions;

using System;

public sealed class LineDuplicateTripException
    : DomainException
{
    public Guid LineId { get; }

    public Guid TripId { get; }

    public LineDuplicateTripException(Guid lineId, Guid tripId)
        : base("Line.DuplicateTrip", $"This trip ({tripId}) is already associated with this line ({lineId}).")
    {
        this.LineId = lineId;
        this.TripId = tripId;
    }
}
