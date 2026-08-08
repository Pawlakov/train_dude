// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Commands.Data.Events;

public class Line
{
    private readonly List<Guid> trips;
    private readonly List<Guid> stations;

    [JsonConstructor]
    private Line(Guid id, int lineNumber, char? lineLetter, ICollection<Guid> trips, ICollection<Guid> stations)
    {
        this.Id = id;
        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
        this.trips = (trips ?? []).ToList();
        this.stations = (stations ?? []).ToList();
    }

    public Guid Id { get; private set; }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public ICollection<Guid> Trips => this.trips.AsReadOnly();

    public ICollection<Guid> Stations => this.stations.AsReadOnly();

    public static Line Create(LineCreated e)
    {
        return new Line(e.LineId, e.LineNumber, e.LineLetter, [], []);
    }

    public void Apply(LineTripAssigned e)
    {
        if (!this.trips.Contains(e.TripId))
        {
            this.trips.Add(e.TripId);
        }
    }

    public void Apply(LineStationAppended e)
    {
        if (this.stations.Count == 0 || this.stations.Last() != e.StationId)
        {
            this.stations.Add(e.StationId);
        }
    }
}