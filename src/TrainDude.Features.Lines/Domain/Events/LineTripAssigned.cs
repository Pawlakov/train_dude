// <copyright file="LineTripAssigned.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain.Events;

using System;

public sealed record LineTripAssigned(Guid LineId, string Who, Guid TripId)
    : ILineEvent
{
    public Guid Id => this.LineId;
}