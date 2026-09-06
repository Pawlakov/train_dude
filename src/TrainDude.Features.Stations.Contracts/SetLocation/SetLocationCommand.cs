// <copyright file="SetLocationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.SetLocation;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Shared.Values;

public sealed record SetLocationCommand(Guid StationId, long Version, Location Location)
    : IUpdateCommand
{
    public const string TypeRoute = "/api/station/location/set";

    public string Route => TypeRoute;

    public Guid Id => this.StationId;
}