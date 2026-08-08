// <copyright file="GetRadiiQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetRadiiQuery;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;

public class GetRadiiQueryResult
    : BasePolymorphicQueryResponse
{
    required public IEnumerable<GetRadiiQueryResultItem> Items { get; init; }
}