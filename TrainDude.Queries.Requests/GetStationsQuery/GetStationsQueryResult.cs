// <copyright file="GetStationsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetStationsQuery;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;

public class GetStationsQueryResult
    : BasePolymorphicQueryResponse
{
    required public IEnumerable<GetStationsQueryResultItem> Items { get; set; }
}