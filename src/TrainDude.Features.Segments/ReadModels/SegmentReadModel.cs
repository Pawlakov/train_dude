// <copyright file="SegmentReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.ReadModels;

using System;
using System.Collections.Generic;

using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Shared.Contracts.Values;

public sealed class SegmentReadModel
{
    public Guid Id { get; set; }

    public double NominalLength { get; set; }

    public double? Haversine { get; set; }

    public int Tracks { get; set; }

    public SegmentEndReference A { get; set; }

    public SegmentEndReference B { get; set; }

    public IReadOnlyList<Location> Course { get; set; }
}