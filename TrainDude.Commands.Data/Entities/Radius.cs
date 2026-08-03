// <copyright file="Radius.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Entities;

using System;

public class Radius
{
    private Radius()
    {
    }

    public Radius(int speed, int minimum)
    {
        if (speed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(speed));
        }

        if (minimum <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minimum));
        }

        this.Speed = speed;
        this.Minimum = minimum;
    }

    public int RadiusId { get; private set; }

    public int Speed { get; private set; }

    public int Minimum { get; private set; }
}