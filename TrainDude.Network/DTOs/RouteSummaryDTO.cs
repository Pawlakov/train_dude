// <copyright file="RouteSummaryDTO.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

public class RouteSummaryDTO
{
    public ObjectId Id { get; init; }

    public string? NameA { get; init; }

    public string? NameB { get; init; }

    public double? Length { get; init; }

    public double? Haversine { get; init; }
}