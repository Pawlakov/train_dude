// <copyright file="GetTripQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Trips;

using TrainDude.Queries.Contracts.Base;

public class GetTripQueryResult
    : BaseEntityLookupQueryResult
{
    required public int TripNumber { get; init; }
}