// <copyright file="RadiusAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Domain;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Radii.Domain.Events;

public class RadiusAggregate
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

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public int Speed { get; private set; }

    public int Minimum { get; private set; }

    public static RadiusCreated Make(Guid id, string who, int speed, int minimum)
    {
        return new RadiusCreated(id, who, speed, minimum);
    }

    public void Apply(RadiusCreated e)
    {
        this.Id = e.Id;
        this.Speed = e.Speed;
        this.Minimum = e.Minimum;

        this.Version++;
    }
}