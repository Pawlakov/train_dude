// <copyright file="Radius.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Shared.Notifications;
using TrainDude.Shared.Notifications.Radii;

public class Radius
    : Aggregate
{
    [JsonConstructor]
    private Radius(Guid id, int speed, int minimum)
        : base(id)
    {
        this.Speed = speed;
        this.Minimum = minimum;
    }

    public int Speed { get; private set; }

    public int Minimum { get; private set; }

    public static Radius Create(Guid radiusId, int speed, int minimum)
    {
        if (speed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(speed));
        }

        if (minimum <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimum));
        }

        var radius = new Radius(radiusId, speed, minimum);
        radius.AddEvent(new RadiusCreatedNotification(radiusId, speed, minimum));
        return radius;
    }

    protected override void Apply(INotification notification)
    {
        switch (notification)
        {
            default:
                throw new NotSupportedException("This event type is not meant for this aggregate.");
        }
    }
}