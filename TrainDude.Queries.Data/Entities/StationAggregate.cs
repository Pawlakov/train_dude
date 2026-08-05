// <copyright file="StationAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Entities;

using TrainDude.Shared.Values;

public class StationAggregate
{
    private StationAggregate()
    {
    }

    public StationAggregate(int stationId, string nameGerman, string? nameGermanNew, Location? location)
    {
        this.StationId = stationId;
        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.Location = location;
    }

    public int StationId { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public Location? Location { get; private set; }
}