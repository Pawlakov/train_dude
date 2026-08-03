// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Entities;

using TrainDude.Shared.Values;

public class Station
{
    private Station()
    {
    }

    public int StationId { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }
    
    public Location? Location { get; set; }
}