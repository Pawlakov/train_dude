// <copyright file="LineAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;
using TrainDude.Domain.Segments;
using TrainDude.Domain.Stations;
using TrainDude.Domain.Trips;

public class LineAggregate
    : BaseAggregate
{
    private readonly List<Guid> segments;
    private readonly List<Guid> trips;

    [JsonConstructor]
    private LineAggregate(Guid id, long version, int lineNumber, char? lineLetter, Guid? startId, ICollection<Guid> segments, ICollection<Guid> trips)
        : base(id, version)
    {
        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
        this.StartId = startId;

        this.segments = (segments ?? []).ToList();
        this.trips = (trips ?? []).ToList();
    }

    public LineAggregate()
        : base()
    {
        this.segments = new List<Guid>();
        this.trips = new List<Guid>();
    }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public Guid? StartId { get; private set; }

    public IReadOnlyList<Guid> Segments => this.segments.AsReadOnly();

    public IReadOnlyList<Guid> Trips => this.trips.AsReadOnly();

    public static LineCreated Make(Guid id, int lineNumber, char? lineLetter)
    {
        return new LineCreated(id, DateTime.UtcNow, lineNumber, lineLetter);
    }

    public LineTripAssigned AssignTrip(Guid tripId)
    {
        this.AssertInitialized(nameof(this.AssignTrip));
        if (this.trips.Contains(tripId))
        {
            throw new LineDuplicateTripException(this.Id, tripId);
        }

        return new LineTripAssigned(this.Id, DateTime.UtcNow, tripId);
    }

    public LineOrderFlipped FlipOrder()
    {
        this.AssertInitialized(nameof(this.FlipOrder));
        return new LineOrderFlipped(this.Id, DateTime.UtcNow);
    }

    public LineSegmentAppended AppendSegment(Guid segmentId) // TODO actually you can enforce rules here if you pass the hole segment aggregate
    {
        this.AssertInitialized(nameof(this.AppendSegment));
        return new LineSegmentAppended(this.Id, DateTime.UtcNow, segmentId);
    }

    public void Apply(BaseAggregateEvent<LineAggregate> @event)
    {
        switch (@event)
        {
            case LineCreated e:
                this.Initialize();
                this.Id = e.Id;
                this.LineNumber = e.LineNumber;
                this.LineLetter = e.LineLetter;
                break;
            case LineTripAssigned e:
                this.trips.Add(e.TripId);
                break;
            case LineOrderFlipped e:
                this.segments.Reverse();
                this.StartId = this.DetermineNewStart();
                break;
            case LineSegmentAppended e:
                this.segments.Add(e.SegmentId);
                break;
            default:
                throw new NotSupportedException("Unknown event type.");
        }

        this.Version++;
    }

    // TODO The logic with axles.
    private Guid DetermineNewStart()
    {
        throw new NotImplementedException();
    }
}