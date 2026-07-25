// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models;

using System.Collections.Generic;

public class Station
{
    public int Id { get; set; }

    public string NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NamePolishOld { get; set; }

    public Coordinates? Location { get; set; }

    public virtual ICollection<RouteExtreme> RouteEnds { get; set; }
}