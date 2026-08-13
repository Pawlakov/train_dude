// <copyright file="GetSegmentQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Segments;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;
using TrainDude.Shared.Values;

public class GetSegmentQueryResult
    : BaseEntityLookupQueryResult
{
    required public string AName { get; init; }

    required public string BName { get; init; }
}