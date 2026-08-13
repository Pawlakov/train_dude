// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;
using System.Collections.Generic;

using LiteDB;

using TrainDude.Shared.Values;

public class Line
{
    [BsonId]
    public Guid LineId { get; set; }

    public int LineNumber { get; set; }

    public char? LineLetter { get; set; }

    public string LineDesignation { get; set; }

    public IReadOnlyCollection<LineTrip> Trips { get; set; }

    public IReadOnlyCollection<LineStation> Stations { get; set; }

    public class LineTrip
    {
        public Guid TripId { get; set; }

        public int TripNumber { get; set; }
    }

    public class LineStation
    {
        public Guid StationId { get; set; }

        public string NameGerman { get; set; }

        public string? NameGermanNew { get; set; }

        public string? NamePolish { get; set; }

        public string? NameRussian { get; set; }

        public Location? Location { get; set; }
    }
}