// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Queries.Data.Aggregates;

using System;

using LiteDB;

public class Line
{
    [BsonId]
    public Guid LineId { get; set; }

    public int LineNumber { get; set; }

    public char? LineLetter { get; set; }

    public string LineDesignation { get; set; }
}