// <copyright file="Location.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models;

using System.Globalization;

public class Location
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[{this.Longitude.ToString(CultureInfo.InvariantCulture)},{this.Latitude.ToString(CultureInfo.InvariantCulture)}]";
    }
}