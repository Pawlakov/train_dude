// <copyright file="GetStationsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Stations;

using System;

/// <summary>
/// A summary of a train station.
/// </summary>
public class GetStationsQueryResultItem
{
    /// <summary>
    /// Gets ID of the station in the database.
    /// </summary>
    required public Guid StationId { get; init; }

    /// <summary>
    /// Gets name of the station if present.
    /// </summary>
    required public string Name { get; init; }

    required public bool HasLocation { get; init; }
}