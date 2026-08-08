// <copyright file="GetLinesQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetLinesQuery;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;

public class GetLinesQueryResult
    : BasePolymorphicQueryResponse
{
    required public IEnumerable<GetLinesQueryResultItem> Items { get; init; }
}