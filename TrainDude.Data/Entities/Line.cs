// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

using System;
using System.Collections.Generic;

public class Line
{
    private readonly List<LineSegment> segments;

    private Line()
    {
        this.segments = new List<LineSegment>();
    }

    public Line(int lineNumber, char? lineLetter)
    {
        if (lineNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(lineNumber));
        }

        if (lineLetter is < 'a' or > 'z')
        {
            throw new ArgumentOutOfRangeException(nameof(lineLetter));
        }

        this.LineNumber = lineNumber;
        this.LineLetter = lineLetter;
    }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public IReadOnlyCollection<LineSegment> Segments => this.segments.AsReadOnly();
}