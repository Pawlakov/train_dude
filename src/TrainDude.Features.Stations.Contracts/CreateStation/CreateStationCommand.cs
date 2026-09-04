// <copyright file="CreateStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.CreateStation;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateStationCommand(string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : ICreateCommand
{
    public const string TypeRoute = "/station/create";

    public string Route => TypeRoute;
}