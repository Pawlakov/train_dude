// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Shared.Notifications;
using TrainDude.Shared.Notifications.Lines;

public class Line
    : Aggregate
{
    private readonly List<Guid> trips;
    private readonly List<Guid> stations;

    [JsonConstructor]
    private Line(Guid id, int lineNumber, char? lineLetter, ICollection<Guid> trips, ICollection<Guid> stations)
        : base(id)
    {
        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
        this.trips = (trips ?? []).ToList();
        this.stations = (stations ?? []).ToList();
    }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public ICollection<Guid> Trips => this.trips.AsReadOnly();

    public ICollection<Guid> Stations => this.stations.AsReadOnly();

    public static Line Create(Guid lineId, int lineNumber, char? lineLetter)
    {
        var line = new Line(lineId, lineNumber, lineLetter, [], []);
        line.AddEvent(new LineCreatedNotification(lineId, lineNumber, lineLetter));
        return line;
    }

    private void AssignTrip(Guid tripId)
    {
        this.AddEvent(new LineTripAssignedNotification(this.Id, tripId));
    }

    private void AppendStation(Guid stationId)
    {
        this.AddEvent(new LineStationAppendedNotification(this.Id, stationId));
    }

    protected override void Apply(INotification notification)
    {
        switch (notification)
        {
            case LineTripAssignedNotification e:
                if (!this.trips.Contains(e.TripId))
                {
                    this.trips.Add(e.TripId);
                }

                break;
            case LineStationAppendedNotification e:
                if (this.stations.Count == 0 || this.stations.Last() != e.StationId)
                {
                    this.stations.Add(e.StationId);
                }

                break;
            default:
                throw new NotSupportedException("This event type is not meant for this aggregate.");
        }
    }
}