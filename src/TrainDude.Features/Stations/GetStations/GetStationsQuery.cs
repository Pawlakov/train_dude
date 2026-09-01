// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStations;

using TrainDude.Features.Base;

/// <summary>
/// A query which returns all stations.
/// </summary>
public sealed record GetStationsQuery : BaseEntityListQuery<GetStationsQueryResult>;