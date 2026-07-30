// <copyright file="GetRadiiQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetRadiiQuery;

public class GetRadiiQueryResultItem
{
    public int RadiusId { get; init; }

    public int Speed { get; init; }

    public int Minimum { get; init; }

    public double MaximumAntiradius { get; init; }
}