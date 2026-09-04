// <copyright file="LineSegmentReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;

public sealed class LineSegmentReference
{
    public Guid Id { get; set; }

    public long Version { get; set; }
}