// <copyright file="GetLineQueryResultStationItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Lines;

using System;

public class GetLineQueryResultStationItem
{
    required public Guid StationId { get; init; }

    required public string Name { get; init; }
}