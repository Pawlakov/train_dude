// <copyright file="CreateStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.CreateStation;

using TrainDude.Features.Base;

public sealed record CreateStationCommand(string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : BaseCreateCommand;