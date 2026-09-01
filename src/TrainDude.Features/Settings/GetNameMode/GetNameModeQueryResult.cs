// <copyright file="GetStationNameModeQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.GetNameMode;

using TrainDude.Shared.Enums;

public class GetNameModeQueryResult
{
    public required NamingPolicy Mode { get; init; }
}