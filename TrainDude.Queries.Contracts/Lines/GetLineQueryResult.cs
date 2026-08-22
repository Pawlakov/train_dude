// <copyright file="GetLineQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Lines;

using System.Collections.Generic;

using TrainDude.Queries.Contracts.Base;

public class GetLineQueryResult
    : BaseEntityLookupQueryResult
{
    required public string LineDesignation { get; init; }

    required public IEnumerable<GetLineQueryResultStationItem> Stations { get; init; }

    required public IEnumerable<GetLineQueryResultTripItem> Trips { get; init; }
}