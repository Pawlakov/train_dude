// <copyright file="Line.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

using System.Collections.Generic;

public class Line
{
    public string LineId { get; set; }

    public virtual ICollection<LineSegment> Segments { get; set; }
}