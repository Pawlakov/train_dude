// <copyright file="SegmentStationReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.ReadModels;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Values;

public sealed class SegmentStationReference
    : IHasAlternativeNames
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public int AxleCount { get; set; }

    public Location? Location { get; set; }

    public string NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NameRussian { get; set; }
}