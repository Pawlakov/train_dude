// <copyright file="SegmentCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Domain.Events;

using System;

using TrainDude.Features.Segments.Domain.Values;

public sealed record SegmentCreated(Guid Id, DateTime When, double NominalLength, int Tracks, SegmentEnd A, SegmentEnd B);