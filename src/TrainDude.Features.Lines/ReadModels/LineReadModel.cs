// <copyright file="LineReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;
using System.Collections.Generic;

using TrainDude.Features.Lines.ReadModels.Values;

public sealed class LineReadModel
{
    public Guid Id { get; set; }

    public int LineNumber { get; set; }

    public char? LineLetter { get; set; }

    public string LineDesignation { get; set; }

    public IReadOnlyList<LineReadModelSegment> Segments { get; set; }

    public IReadOnlyList<LineReadModelStation> Stations { get; set; }

    public IReadOnlyList<LineReadModelTrip> Trips { get; set; }
}