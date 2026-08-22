// <copyright file="Radius.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Events.Radii;

public class Radius
    : AggregateBase
{
    [JsonConstructor]
    private Radius(Guid id, long version, int speed, int minimum)
    {
        this.Id = id;
        this.Version = version;

        this.Speed = speed;
        this.Minimum = minimum;
    }

    public Radius()
    {
    }

    public int Speed { get; private set; }

    public int Minimum { get; private set; }

    public static RadiusCreated Make(Guid id, int speed, int minimum)
    {
        if (speed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(speed));
        }

        if (minimum <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimum));
        }

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