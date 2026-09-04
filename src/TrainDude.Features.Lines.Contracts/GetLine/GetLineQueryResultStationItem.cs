// <copyright file="GetLineQueryResultStationItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLine;

using System;

public class GetLineQueryResultStationItem
{
    public required Guid StationId { get; init; }

    public required string Name { get; init; }
}