// <copyright file="GetLinesQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Lines;

using System;

public class GetLinesQueryResultItem
{
    required public Guid LineId { get; init; }

    required public string LineDesignation { get; init; }
}