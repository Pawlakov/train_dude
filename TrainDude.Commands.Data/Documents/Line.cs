// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Commands.Data.Events;

public class Line
{
    [JsonConstructor]
    private Line(Guid id, int lineNumber, char? lineLetter)
    {
        this.Id = id;
        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
    }

    public Guid Id { get; private set; }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public static Line Create(LineCreated e)
    {
        return new Line(e.LineId, e.LineNumber, e.LineLetter);
    }
}