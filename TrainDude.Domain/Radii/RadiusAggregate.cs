// <copyright file="RadiusAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Radii;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;

public class RadiusAggregate
    : BaseAggregate
{
    [JsonConstructor]
    private RadiusAggregate(Guid id, long version, int speed, int minimum)
    {
        this.Id = id;
        this.Version = version;

        this.Speed = speed;
        this.Minimum = minimum;
    }

    public RadiusAggregate()
    {
    }

    public int Speed { get; private set; }

    public int Minimum { get; private set; }

    public static RadiusCreated Make(Guid id, int speed, int minimum)
    {
        return new RadiusCreated(id, DateTime.UtcNow, speed, minimum);
    }

    public void Apply(RadiusCreated e)
    {
        this.Id = e.Id;
        this.Speed = e.Speed;
        this.Minimum = e.Minimum;

        this.Version++;
    }
}