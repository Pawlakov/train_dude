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

internal class Route
{
    public ObjectId Id { get; set; }

    public ObjectId A { get; set; }

    public ObjectId B { get; set; }

    public double? NominalLength { get; set; }

    public int? Tracks { get; set; }
}