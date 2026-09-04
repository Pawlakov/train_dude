// <copyright file="LineTripReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;

public sealed class LineTripReference
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public int Number { get; set; }
}