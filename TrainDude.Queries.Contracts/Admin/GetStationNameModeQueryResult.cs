// <copyright file="GetStationNameModeQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Admin;

using TrainDude.Queries.Contracts.Base;
using TrainDude.Shared.Values;

public class GetStationNameModeQueryResult
    : BasePolymorphicQueryResult
{
    required public StationNameMode Mode { get; init; }
}