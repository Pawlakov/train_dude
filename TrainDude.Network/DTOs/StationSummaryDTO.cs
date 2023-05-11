// <copyright file="StationSummaryDTO.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

/// <summary>
/// A summary of a train station.
/// </summary>
public class StationSummaryDTO
{
    /// <summary>
    /// Gets ID of the station in the database.
    /// </summary>
    public ObjectId Id { get; init; }

    /// <summary>
    /// Gets name of the station if present.
    /// </summary>
    public string? Name { get; init; }
}