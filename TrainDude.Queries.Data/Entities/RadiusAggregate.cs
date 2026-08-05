// <copyright file="RadiusAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Entities;

public class RadiusAggregate
{
    private RadiusAggregate()
    {
    }

    public RadiusAggregate(int radiusId, int speed, int minimum)
    {
        this.RadiusId = radiusId;
        this.Speed = speed;
        this.Minimum = minimum;
    }

    public int RadiusId { get; private set; }

    public int Speed { get; private set; }

    public int Minimum { get; private set; }
}