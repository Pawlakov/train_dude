// <copyright file="CreateRadiusCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Contracts.CreateRadius;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateRadiusCommand(int Speed, int Minimum) : ICreateCommand
{
    public const string TypeRoute = "/api/radii/create";

    public string Route => TypeRoute;
}