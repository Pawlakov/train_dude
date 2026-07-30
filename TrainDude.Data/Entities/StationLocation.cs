// <copyright file="StationLocation.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

public class StationLocation
    : Location
{
    public int StationId { get; set; }

    public Station Station { get; set; }
}