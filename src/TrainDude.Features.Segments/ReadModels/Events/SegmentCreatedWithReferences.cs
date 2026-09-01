// <copyright file="SegmentCreatedWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.ReadModels.Events;

using System;

using TrainDude.Features.Segments.Domain.Values;

public sealed record SegmentCreatedWithReferences(Guid Id, DateTime When, double NominalLength, int Tracks, SegmentEndReference A, SegmentEndReference B);