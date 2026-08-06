// <copyright file="GetSegmentQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetSegmentQuery;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;
using TrainDude.Shared.Values;

public class GetSegmentQueryResult
    : BasePolymorphicQueryResponse
{
    required public string AName { get; init; }

    required public string BName { get; init; }

    required public Location? ALocation { get; init; }

    required public Location? BLocation { get; init; }

    required public ICollection<Location> Vertices { get; init; }
}