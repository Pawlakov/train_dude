// <copyright file="GetStationNameModeQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Admin;

using System;

using TrainDude.Queries.Requests.Base;
using TrainDude.Shared.Values;

public class GetStationNameModeQueryResult
    : BasePolymorphicQueryResult
{
    required public StationNameMode Mode { get; init; }
}