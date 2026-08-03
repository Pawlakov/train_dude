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
    public int SegmentId { get; init; }

    public string AName { get; init; }

    public string BName { get; init; }

    public Location? ALocation { get; set; }

    public Location? BLocation { get; set; }

    public IEnumerable<Location> Vertices { get; set; }
}