// <copyright file="StationSummaryDTO.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.DTOs;

using TrainDude.Data.Models;

/// <summary>
/// A summary of a train station.
/// </summary>
public class StationSummaryDTO
{
    /// <summary>
    /// Gets ID of the station in the database.
    /// </summary>
    public int StationId { get; init; }

    /// <summary>
    /// Gets name of the station if present.
    /// </summary>
    public string? Name { get; init; }

    public bool HasLocation { get; init; }
}