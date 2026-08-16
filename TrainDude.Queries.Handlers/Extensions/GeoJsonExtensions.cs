// <copyright file="GeoJsonExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

using TrainDude.Integration.Values;

internal static class GeoJsonExtensions
{
    internal static double Haversine(this IEnumerable<(Location A, Location B)> segments)
    {
        // TODO dostosować do do geoidy i pary dwóch punktów tylko
        var pointPairs = segments.ToArray();
        var earthRadius = 6371.2;
        var total = 0.0;
        foreach (var (a, b) in pointPairs)
        {
            var latitudeDifference = ToRadians(b.Latitude - a.Latitude);
            var longitudeDifference = ToRadians(b.Longitude - a.Longitude);
            var something = (Math.Sin(latitudeDifference / 2) * Math.Sin(latitudeDifference / 2)) + (Math.Cos(ToRadians(a.Latitude)) * Math.Cos(ToRadians(b.Latitude)) * Math.Sin(longitudeDifference / 2) * Math.Sin(longitudeDifference / 2));

            var c = 2 * Math.Atan2(Math.Sqrt(something), Math.Sqrt(1 - something));
            total += earthRadius * c;
        }

        return total;
    }

    // https://medium.com/theburningmonk-com/net-tips-use-linq-to-create-pairs-of-adjacent-elements-from-a-collection-a3e9c04ed5b
    internal static IEnumerable<(Location A, Location B)> Segments(this IEnumerable<Location> points)
    {
        var previous = (Location?)null;
        foreach (var point in points)
        {
            if (previous.HasValue)
            {
                yield return (previous.Value, point);
            }

            previous = point;
        }
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}