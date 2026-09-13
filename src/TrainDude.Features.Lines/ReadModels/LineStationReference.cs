// <copyright file="LineStationReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Values;

public sealed class LineStationReference
    : IHasAlternativeNames
{
    public Guid Id { get; set; }

    public int AxleCount { get; set; }

    public Location? Location { get; set; }

    public string NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NameRussian { get; set; }
}