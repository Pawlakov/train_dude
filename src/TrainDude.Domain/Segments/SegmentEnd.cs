// <copyright file="SegmentEnd.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Segments;

using System;

public record struct SegmentEnd(Guid Id, int Axle, bool Pole);