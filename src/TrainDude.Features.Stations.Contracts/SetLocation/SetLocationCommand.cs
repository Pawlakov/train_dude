// <copyright file="SetLocationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.SetLocation;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Values;

public sealed record SetLocationCommand(Location Location)
    : ISpecificCommand
{
    public const string TypeRoute = "/api/stations/{0}/set-location";

    public static string Route => TypeRoute;
}