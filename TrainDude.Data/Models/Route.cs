// <copyright file="Route.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models;

using System.Collections.Generic;

public class Route
{
    public int Id { get; set; }

    public double? NominalLength { get; set; }

    public virtual ICollection<RouteExtreme> Ends { get; set; }
}