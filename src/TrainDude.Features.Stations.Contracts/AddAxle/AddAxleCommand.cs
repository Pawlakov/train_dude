// <copyright file="AddAxleCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.AddAxle;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record AddAxleCommand(Guid Id)
    : ISpecificCommand
{
    public const string TypeRoute = "/api/stations/{id}/add-axle";

    public static string Route => TypeRoute;
}