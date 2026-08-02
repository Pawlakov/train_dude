// <copyright file="Trip.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

using System;

public class Trip
{
    private Trip()
    {
    }

    public Trip(int tripNumber)
    {
        if (tripNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tripNumber));
        }

        this.TripNumber = tripNumber;
    }

    public int TripNumber { get; private set; }
}