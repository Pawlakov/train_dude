// <copyright file="RadiusSummaryDTO.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.DTOs;

using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;

public class RadiusSummaryDTO
{
    public ObjectId Id { get; init; }

    public int Speed { get; init; }

    public int Minimum { get; init; }

    public double MaximumAntiradius { get; init; }
}