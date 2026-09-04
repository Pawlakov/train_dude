// <copyright file="LineReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.Domain.Values;
using TrainDude.Features.Lines.ReadModels.Events;

public sealed class LineReadModel
{
    private readonly List<LineSegment> segments;
    private readonly List<LineStation> stations;
    private readonly List<LineTrip> trips;

    [JsonConstructor]
    private LineReadModel(Guid id, long version, int lineNumber, char? lineLetter, string lineDesignation, List<LineSegment> segments, List<LineStation> stations, List<LineTrip> trips)
    {
        this.Id = id;
        this.Version = version;

        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
        this.LineDesignation = lineDesignation;
        this.segments = segments ?? [];
        this.stations = stations ?? [];
        this.trips = trips ?? [];
    }

    public LineReadModel()
    {
        this.segments = new List<LineSegment>();
        this.stations = new List<LineStation>();
        this.trips = new List<LineTrip>();
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public string LineDesignation { get; private set; }

    public IReadOnlyList<LineSegment> Segments => this.segments.AsReadOnly();

    public IReadOnlyList<LineStation> Stations => this.stations.AsReadOnly();

    public IReadOnlyList<LineTrip> Trips => this.trips.AsReadOnly();

    public void Apply(LineCreated e)
    {
        this.Id = e.Id;
        this.LineNumber = e.LineNumber;
        this.LineLetter = e.LineLetter;
        this.LineDesignation = $"{e.LineNumber}{e.LineLetter}";

        this.Version++;
    }

    public void Apply(LineTripAssignedWithReferences e)
    {
        this.trips.Add(e.Trip);

        this.Version++;
    }

    public void Apply(LineSegmentAppendedWithReferences e)
    {
        this.segments.Add(e.Segment);
        if (this.stations.Last() == e.Segment.A)
        {
            this.stations.Add(e.Segment.B);
        }
        else
        {
            this.stations.Add(e.Segment.A);
        }

        this.Version++;
    }
}