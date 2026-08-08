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

    [JsonConstructor]
    private Line(Guid id, int lineNumber, char? lineLetter, ICollection<Guid> trips)
    {
        this.trips = (trips ?? []).ToList();

        this.Id = id;
        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
    }

    public Guid Id { get; private set; }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    [JsonInclude]
    public ICollection<Guid> Trips => this.trips.AsReadOnly();

    public static Line Create(LineCreated e)
    {
        return new Line(e.LineId, e.LineNumber, e.LineLetter, []);
    }

    public void Apply(LineTripAssigned e)
    {
        if (!this.trips.Contains(e.TripId))
        {
            this.trips.Add(e.TripId);
        }
    }
}