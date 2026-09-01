// <copyright file="GetStationsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStations;

using System;

/// <summary>
/// A summary of a train station.
/// </summary>
public class GetStationsQueryResultItem
{
    /// <summary>
    /// Gets ID of the station in the database.
    /// </summary>
    public required Guid StationId { get; init; }

    /// <summary>
    /// Gets name of the station if present.
    /// </summary>
    public required string Name { get; init; }

    public required bool HasLocation { get; init; }
}