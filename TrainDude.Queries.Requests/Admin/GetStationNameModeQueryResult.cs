// <copyright file="GetStationNameModeQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Admin;

using TrainDude.Integration.Values;
using TrainDude.Queries.Requests.Base;

public class GetStationNameModeQueryResult
    : BasePolymorphicQueryResult
{
    required public StationNameMode Mode { get; init; }
}