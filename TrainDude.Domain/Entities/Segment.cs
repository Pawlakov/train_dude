// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

/*namespace TrainDude.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;

using TrainDude.Domain.Events.Values;

public class Segment
{
    private readonly List<SegmentExtreme> extremes;
    private readonly List<SegmentVertex> vertices;

    private Segment()
    {
        this.extremes = new List<SegmentExtreme>();
        this.vertices = new List<SegmentVertex>();
    }

    public Segment(double? nominalLength)
        : this()
    {
        if (nominalLength.HasValue && nominalLength.Value <= 0.0)
        {
            throw new ArgumentOutOfRangeException(nameof(nominalLength));
        }

        this.NominalLength = nominalLength;
    }

    public int SegmentId { get; private set; }

    public double? NominalLength { get; private set; }

    public IReadOnlyCollection<SegmentExtreme> Extremes => this.extremes.AsReadOnly();

    public IReadOnlyCollection<SegmentVertex> Vertices => this.vertices.AsReadOnly();

    public void AddExtremes(Station startStation, Station endStation)
    {
        if (startStation is null)
        {
            throw new ArgumentNullException(nameof(startStation));
        }

        if (endStation is null)
        {
            throw new ArgumentNullException(nameof(endStation));
        }

        if (this.extremes.Count > 0)
        {
            throw new InvalidOperationException("This method is meant only for adding extremes to a segment which has none.");
        }

        this.extremes.Add(new SegmentExtreme(false, startStation));
        this.extremes.Add(new SegmentExtreme(true, endStation));
    }

    public void AddVertices(IEnumerable<Location> vertexLocations)
    {
        if (this.vertices.Count > 0)
        {
            throw new InvalidOperationException("This method is meant only for adding vertices to a segment which has none.");
        }

        foreach (var (location, index) in vertexLocations.Select((x, index) => (x, index)))
        {
            this.vertices.Add(new SegmentVertex(index + 1, location));
        }
    }
}*/