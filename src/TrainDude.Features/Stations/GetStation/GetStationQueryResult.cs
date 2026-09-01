// <copyright file="GetStationQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStation;

using TrainDude.Features.Base;
using TrainDude.Shared.Values;

public class GetStationQueryResult
    : BaseEntityLookupQueryResult
{
    public required string Name { get; init; }

    public required Location? Location { get; init; }

    public required int AxleCount { get; init; }
}