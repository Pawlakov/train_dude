// <copyright file="RouteSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Models.Seed;

using System.Collections.Generic;

internal class SegmentSeed
{
    required public ExtremeSeed A { get; set; }

    required public ExtremeSeed B { get; set; }

    public double Length { get; set; }

    public int Tracks { get; set; }

    public List<MidPoint>? MidPoints { get; set; }

    internal class ExtremeSeed
    {
        public int StationId { get; set; }

        public int? Axle { get; set; }

        public bool Pole { get; set; }
    }

    internal class MidPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}