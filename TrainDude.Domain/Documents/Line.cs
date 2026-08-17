// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Documents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Domain.Events.Lines;
using TrainDude.Domain.Exceptions;

public class Line
    : AggregateBase
{
    private readonly List<Guid> trips;
    private readonly List<Guid> stations;

    [JsonConstructor]
    private Line(Guid id, long version, int lineNumber, char? lineLetter, ICollection<Guid> trips, ICollection<Guid> stations)
    {
        this.Id = id;
        this.Version = version;

        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
        this.trips = (trips ?? []).ToList();
        this.stations = (stations ?? []).ToList();
    }

    public Line()
    {
    }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public ICollection<Guid> Trips => this.trips.AsReadOnly();

    public ICollection<Guid> Stations => this.stations.AsReadOnly();

    public static LineCreated Make(Guid id, int lineNumber, char? lineLetter)
    {
        return new LineCreated(id, lineNumber, lineLetter);
    }

    public LineTripAssigned AssignTrip(Guid tripId)
    {
        if (this.trips.Contains(tripId))
        {
            throw new LineDuplicateTripException(this.Id, tripId);
        }

        return new LineTripAssigned(this.Id, tripId);
    }

    public LineStationAppended AppendStation(Guid stationId)
    {
        if (this.stations.Count != 0 && this.stations.Last() == stationId)
        {
            throw new LineDuplicateStationException(this.Id, stationId);
        }

        return new LineStationAppended(this.Id, stationId);
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

    public void Apply(LineStationAppended e)
    {
        this.stations.Add(e.StationId);

        this.Version++;
    }
}