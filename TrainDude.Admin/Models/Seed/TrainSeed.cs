// <copyright file="TrainSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Admin.Models.Seed;

using TrainDude.Admin.Enums;

internal class TrainSeed
{
    public int Number { get; set; }

    public TrainType Type { get; set; }

    public TrainClass[]? Classes { get; set; }

    public TrainScheduleSeed[]? Schedules { get; set; }
}