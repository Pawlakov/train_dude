// <copyright file="RouteExtreme.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models;

public class RouteExtreme
{
    public bool IsEnd { get; set; }

    public int StationId { get; set; }

    public virtual Station? Station { get; set; }

    public int RouteId { get; set; }

    public virtual Route? Route { get; set; }
}