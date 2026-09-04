// <copyright file="SegmentEndReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Domain.Values;

using System;

using TrainDude.Shared.Values;

public record struct SegmentEndReference(Guid Id, int Axle, bool Pole, Location? Location, string Name);