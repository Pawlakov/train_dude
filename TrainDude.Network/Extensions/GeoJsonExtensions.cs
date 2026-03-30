namespace TrainDude.Network.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver.GeoJsonObjectModel;

internal static class GeoJsonExtensions
{
    internal static double Haversine(this IEnumerable<(GeoJson2DGeographicCoordinates, GeoJson2DGeographicCoordinates)> segments)
    {
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
    internal static IEnumerable<(GeoJson2DGeographicCoordinates A, GeoJson2DGeographicCoordinates B)> Segments(this IEnumerable<GeoJson2DGeographicCoordinates?> points)
    {
        var pointArray = points.Where(x => x != null).ToArray();
        if (pointArray.Length > 1)
        {
            return pointArray.Skip(1).Zip(pointArray, (a, b) => (a, b))!;
        }

        return Enumerable.Empty<(GeoJson2DGeographicCoordinates, GeoJson2DGeographicCoordinates)>();
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}