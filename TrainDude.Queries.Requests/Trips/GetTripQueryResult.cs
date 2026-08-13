// <copyright file="GetTripQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Trips;

using TrainDude.Queries.Requests.Base;

public class GetTripQueryResult
    : BaseEntityLookupQueryResult
{
    required public int TripNumber { get; init; }
}