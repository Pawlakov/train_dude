// <copyright file="TrainScheduleSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Seed;

using TrainDude.Shared.Values;

internal class TrainScheduleSeed
{
    public TrainScheduleDays[]? Days { get; set; }

    public string? Start { get; set; }

    public TrainScheduleEventSeed[]? Events { get; set; }
}