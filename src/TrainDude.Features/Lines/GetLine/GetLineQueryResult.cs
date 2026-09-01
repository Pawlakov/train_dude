// <copyright file="GetLineQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLine;

using System.Collections.Generic;

using TrainDude.Features.Base;

public class GetLineQueryResult
    : BaseEntityLookupQueryResult
{
    public required string LineDesignation { get; init; }

    public required IEnumerable<GetLineQueryResultStationItem> Stations { get; init; }

    public required IEnumerable<GetLineQueryResultTripItem> Trips { get; init; }
}