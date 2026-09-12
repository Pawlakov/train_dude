// <copyright file="TripSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Admin.SeedModel;

using TrainDude.Features.Shared.Contracts.Enums;

internal class TripSeed
{
    public int Number { get; set; }

    public TripType Type { get; set; }

    public bool? LimitedStorage { get; set; }

    public CarriageClass[]? Classes { get; set; }

    public TripScheduleSeed[]? Schedules { get; set; }
}