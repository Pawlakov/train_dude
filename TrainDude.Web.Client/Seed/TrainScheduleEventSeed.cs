// <copyright file="TrainScheduleEventSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Seed;

using TrainDude.Integration.Values;

internal class TrainScheduleEventSeed
{
    public TrainScheduleEventType Type { get; set; }

    public int Station { get; set; }

    public int? At { get; set; }

    public bool? OnRequest { get; set; }
}