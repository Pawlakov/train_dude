// <copyright file="GeoJsonExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Values;

using System;
using System.Collections.Generic;
using System.Linq;

public static class GeoJsonExtensions
{
    private const double earthRadius = 6371.2;

    public static double Haversine(this Location a, Location b)
    {
        // TODO dostosować do do geoidy i pary dwóch punktów tylko
        var latitudeDifference = ToRadians(b.Latitude - a.Latitude);
        var longitudeDifference = ToRadians(b.Longitude - a.Longitude);
        var something = (Math.Sin(latitudeDifference / 2) * Math.Sin(latitudeDifference / 2)) + (Math.Cos(ToRadians(a.Latitude)) * Math.Cos(ToRadians(b.Latitude)) * Math.Sin(longitudeDifference / 2) * Math.Sin(longitudeDifference / 2));

        var c = 2 * Math.Atan2(Math.Sqrt(something), Math.Sqrt(1 - something));
        return earthRadius * c;
    }

    // https://medium.com/theburningmonk-com/net-tips-use-linq-to-create-pairs-of-adjacent-elements-from-a-collection-a3e9c04ed5b
    public static double Haversine(this IEnumerable<Location> points)
    {
        var total = 0.0;
        var previous = (Location?)null;
        foreach (var point in points)
        {
            if (previous.HasValue)
            {
                total += previous.Value.Haversine(point);
            }

            previous = point;
        }

        return total;
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}