// <copyright file="LineSegmentReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Values;

public sealed class LineSegmentReference
{
    public Guid Id { get; set; }

    public Guid AId { get; set; }

    public Guid BId { get; set; }

    public IReadOnlyList<Location> Course { get; set; }
}