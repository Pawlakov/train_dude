// <copyright file="AddAxleCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.AddAxle;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record AddAxleCommand()
    : IUpdateCommand
{
    public const string TypeRoute = "/api/stations/{id}/add-axle/{version}";

    public string Route => TypeRoute;
}