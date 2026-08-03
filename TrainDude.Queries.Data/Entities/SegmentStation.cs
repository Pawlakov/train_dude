// <copyright file="SegmentStation.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Queries.Data.Entities;

using TrainDude.Shared.Values;

public class SegmentStation
{
    public int SegmentStationId { get; private set; }

    public int StationId { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public Location? Location { get; private set; }
}