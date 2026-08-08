// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;
using System.Collections.Generic;

using LiteDB;

public class Line
{
    [BsonId]
    public Guid LineId { get; set; }

    public int LineNumber { get; set; }

    public char? LineLetter { get; set; }

    public string LineDesignation { get; set; }

    public IReadOnlyCollection<LineTrip> Trips { get; set; }

    public class LineTrip
    {
        public Guid TripId { get; set; }

        public int TripNumber { get; set; }
    }
}