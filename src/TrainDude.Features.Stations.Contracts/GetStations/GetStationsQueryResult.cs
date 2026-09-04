// <copyright file="GetStationsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStations;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetStationsQueryResult(IReadOnlyList<GetStationsQueryResultItem> Items) : IListQueryResult<GetStationsQueryResultItem>;