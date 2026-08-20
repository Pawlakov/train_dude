// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using LiteDB;

using TrainDude.Integration.Values;

public class Line
    : IVersionedDocument
{
    [BsonId]
    public Guid Id { get; set; }

    public long Version { get; set; }

    public int LineNumber { get; set; }

    public char? LineLetter { get; set; }

    public string LineDesignation { get; set; }

    public ImmutableList<LineTrip> Trips { get; set; }

    public ImmutableList<LineStation> Stations { get; set; }

    public class LineTrip
    {
        public Guid TripId { get; set; }

        public int TripNumber { get; set; }
    }

    public class LineStation
    {
        public Guid StationId { get; set; }

        public string Name { get; set; }

        public Location? Location { get; set; }
    }
}