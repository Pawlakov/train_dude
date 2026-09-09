// <copyright file="LineReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;
using System.Collections.Generic;

using TrainDude.Features.Lines.Domain.Values;

public sealed class LineReadModel
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public int LineNumber { get; set; }

    public char? LineLetter { get; set; }

    public string LineDesignation { get; set; }

    public IReadOnlyList<LineSegment> Segments { get; set; }

    public IReadOnlyList<LineStation> Stations { get; set; }

    public IReadOnlyList<LineTrip> Trips { get; set; }
}