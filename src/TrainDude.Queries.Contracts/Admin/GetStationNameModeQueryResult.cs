// <copyright file="GetStationNameModeQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Admin;

using TrainDude.Queries.Contracts.Base;
using TrainDude.Shared;
using TrainDude.Shared.Enums;

public class GetStationNameModeQueryResult
    : BasePolymorphicQueryResult
{
    public required StationNameMode Mode { get; init; }
}