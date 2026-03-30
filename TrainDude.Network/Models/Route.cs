// <copyright file="Route.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

internal class Route
{
    public ObjectId Id { get; set; }

    required public EndPoint A { get; set; }

    required public EndPoint B { get; set; }

    public double? NominalLength { get; set; }

    public int? Tracks { get; set; }

    required public List<MidPoint> MidPoints { get; set; }

    internal class EndPoint
    {
        public ObjectId StationId { get; set; }

        public int? Axle { get; set; }

        public bool Pole { get; set; }
    }

    internal class MidPoint
    {
        required public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
    }
}