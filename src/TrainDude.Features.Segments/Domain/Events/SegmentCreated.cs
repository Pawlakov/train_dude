// <copyright file="SegmentCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Domain.Events;

using System;

using TrainDude.Features.Segments.Domain.Values;

public sealed record SegmentCreated(Guid SegmentId, string Who, double NominalLength, int Tracks, SegmentEnd A, SegmentEnd B)
    : ISegmentEvent
{
    public Guid Id => this.SegmentId;
}