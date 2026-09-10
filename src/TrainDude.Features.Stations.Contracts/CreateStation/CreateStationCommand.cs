// <copyright file="CreateStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.CreateStation;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateStationCommand(Guid StationId, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian)
    : IGeneralCommand
{
    public const string TypeRoute = "/api/stations/create";

    public static string Route => TypeRoute;
}