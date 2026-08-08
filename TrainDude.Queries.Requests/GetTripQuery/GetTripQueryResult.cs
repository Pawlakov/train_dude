// <copyright file="GetTripQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetTripQuery;

using TrainDude.Queries.Requests.Base;

public class GetTripQueryResult
    : BasePolymorphicQueryResponse
{
    required public int TripNumber { get; init; }
}