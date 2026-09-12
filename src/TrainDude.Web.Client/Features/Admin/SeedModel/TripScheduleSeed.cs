// <copyright file="TripScheduleSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Admin.SeedModel;

using TrainDude.Features.Shared.Contracts.Enums;

internal class TripScheduleSeed
{
    public TripScheduleDays[]? Days { get; set; }

    public string? Start { get; set; }

    public TripScheduleEventSeed[]? Events { get; set; }
}