// <copyright file="GetSegmentsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetSegmentsQuery;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;

public class GetSegmentsQueryResult
    : BasePolymorphicQueryResponse
{
    required public IEnumerable<GetSegmentsQueryResultItem> Items { get; init; }
}