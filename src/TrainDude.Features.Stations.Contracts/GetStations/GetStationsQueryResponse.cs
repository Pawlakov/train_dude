// <copyright file="GetStationsQueryResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStations;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetStationsQueryResponse(IReadOnlyList<GetStationsQueryResultItem> Items) : IListQueryResponse<GetStationsQueryResultItem>;