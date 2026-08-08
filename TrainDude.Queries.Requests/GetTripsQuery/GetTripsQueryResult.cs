// <copyright file="GetTripsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetTripsQuery;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;

public class GetTripsQueryResult
    : BasePolymorphicQueryResponse
{
    required public IEnumerable<GetTripsQueryResultItem> Items { get; init; }
}