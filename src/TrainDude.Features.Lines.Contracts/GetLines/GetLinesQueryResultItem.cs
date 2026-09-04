// <copyright file="GetLinesQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLines;

using System;

public class GetLinesQueryResultItem
{
    public required Guid LineId { get; init; }

    public required string LineDesignation { get; init; }
}