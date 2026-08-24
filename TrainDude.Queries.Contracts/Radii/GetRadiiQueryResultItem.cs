// <copyright file="GetRadiiQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Radii;

using System;

public class GetRadiiQueryResultItem
{
    public required Guid RadiusId { get; init; }

    public required int Speed { get; init; }

    public required int Minimum { get; init; }

    public required double MaximumAntiradius { get; init; }
}