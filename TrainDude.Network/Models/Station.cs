// <copyright file="Station.cs" company="Pawlakov">
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

internal class Station
{
    public ObjectId Id { get; set; }

    public string? NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NamePolishOld { get; set; }

    public GeoJsonPoint<GeoJson2DGeographicCoordinates>? Location { get; set; }

    public double? Altitude { get; set; }
}