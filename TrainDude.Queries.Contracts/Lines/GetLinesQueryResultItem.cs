// <copyright file="GetLinesQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Lines;

using System;

public class GetLinesQueryResultItem
{
    public required Guid LineId { get; init; }

    public required string LineDesignation { get; init; }
}