// <copyright file="StationSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Models.Seed;

internal class StationSeed
{
    public int Id { get; set; }

    public string? NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NamePolishOld { get; set; }

    public double Elevation { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}