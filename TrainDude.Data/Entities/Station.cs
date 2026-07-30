// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

using System.Collections.Generic;

public class Station
{
    public int StationId { get; set; }

    public string NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NamePolishOld { get; set; }

    public StationLocation? Location { get; set; }

    public virtual ICollection<SegmentExtreme> SegmentExtremes { get; set; }
}