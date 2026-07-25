// <copyright file="TrainScheduleEventSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Admin.Models.Seed;

using TrainDude.Admin.Enums;

internal class TrainScheduleEventSeed
{
    public TrainScheduleEventType Type { get; set; }

    public int Station { get; set; }

    public int? At { get; set; }
}