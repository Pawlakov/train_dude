// <copyright file="GetRadiiQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Radii;

using System;

public class GetRadiiQueryResultItem
{
    required public Guid RadiusId { get; init; }

    required public int Speed { get; init; }

    required public int Minimum { get; init; }

    required public double MaximumAntiradius { get; init; }
}