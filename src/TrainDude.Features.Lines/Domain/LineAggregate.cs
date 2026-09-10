// <copyright file="LineAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.Domain.Exceptions;
using TrainDude.Features.Lines.ReadModels;

public class LineAggregate
{
    private readonly List<Guid> segments;
    private readonly List<Guid> trips;

    [JsonConstructor]
    private LineAggregate(Guid id, long version, int lineNumber, char? lineLetter, Guid? startId, ICollection<Guid> segments, ICollection<Guid> trips)
    {
        this.Id = id;
        this.Version = version;

        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
        this.StartId = startId;

        this.segments = (segments ?? []).ToList();
        this.trips = (trips ?? []).ToList();
    }

    public LineAggregate()
    {
        this.segments = new List<Guid>();
        this.trips = new List<Guid>();
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public Guid? StartId { get; private set; }

    public IReadOnlyList<Guid> Segments => this.segments.AsReadOnly();

    public IReadOnlyList<Guid> Trips => this.trips.AsReadOnly();

    public static LineCreated Make(Guid id, string who, int lineNumber, char? lineLetter)
    {
        return new LineCreated(id, who, lineNumber, lineLetter);
    }

    public LineTripAssigned AssignTrip(string who, LineTripReference trip)
    {
        if (this.trips.Contains(trip.Id))
        {
            throw new LineDuplicateTripException(this.Id, trip.Id);
        }

        return new LineTripAssigned(this.Id, who, trip.Id);
    }

    public LineSegmentAppended AppendSegment(string who, LineSegmentReference segment)
    {
        return new LineSegmentAppended(this.Id, who, segment.Id);
    }

    public void Apply(LineCreated e)
    {
        this.Id = e.Id;
        this.LineNumber = e.LineNumber;
        this.LineLetter = e.LineLetter;

        this.Version++;
    }

    public void Apply(LineTripAssigned e)
    {
        this.trips.Add(e.TripId);

        this.Version++;
    }

    public void Apply(LineSegmentAppended e)
    {
        this.segments.Add(e.SegmentId);

        this.Version++;
    }
}