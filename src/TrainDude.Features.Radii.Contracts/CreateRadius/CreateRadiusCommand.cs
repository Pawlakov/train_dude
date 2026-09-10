// <copyright file="CreateRadiusCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Contracts.CreateRadius;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateRadiusCommand(Guid RadiusId, int Speed, int Minimum)
    : IGeneralCommand
{
    public const string TypeRoute = "/api/radii/create";

    public static string Route => TypeRoute;
}