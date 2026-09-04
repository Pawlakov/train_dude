// <copyright file="AddAxleCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.AddAxle;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record AddAxleCommand(Guid StationId, long Version)
    : IUpdateCommand
{
    public const string TypeRoute = "/station/axle/add";

    public string Route => TypeRoute;

    public Guid Id => this.StationId;
}