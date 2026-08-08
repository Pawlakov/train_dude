// <copyright file="TripSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Seed;

using TrainDude.Shared.Values;

internal class TripSeed
{
    public int Number { get; set; }

    public TrainType Type { get; set; }

    public bool? LimitedStorage { get; set; }

    public TrainClass[]? Classes { get; set; }

    public TrainScheduleSeed[]? Schedules { get; set; }
}