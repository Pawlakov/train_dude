// <copyright file="SegmentSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Seed;

using System.Collections.Generic;

internal class SegmentSeed
{
    public int Id { get; set; }

    public required ExtremeSeed A { get; set; }

    public required ExtremeSeed B { get; set; }

    public double Length { get; set; }

    public int Tracks { get; set; }

    public List<VertexSeed>? Course { get; set; }

    internal class ExtremeSeed
    {
        public int StationId { get; set; }

        public int? Axle { get; set; }

        public bool Pole { get; set; }
    }

    internal class VertexSeed
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}