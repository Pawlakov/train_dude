// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

using System;
using System.Collections.Generic;

using TrainDude.Shared.Values;

public class Station
{
    private readonly List<SegmentExtreme> segmentExtremes;

    private Station()
    {
        this.segmentExtremes = new List<SegmentExtreme>();
    }

    public Station(string nameGerman, string? nameGermanNew, string? namePolish, string? namePolishOld, Location? location)
        : this()
    {
        if (string.IsNullOrWhiteSpace(nameGerman))
        {
            throw new ArgumentOutOfRangeException(nameof(nameGerman));
        }

        if (nameGermanNew is not null && string.IsNullOrWhiteSpace(nameGermanNew))
        {
            throw new ArgumentOutOfRangeException(nameof(nameGermanNew));
        }

        if (namePolish is not null && string.IsNullOrWhiteSpace(namePolish))
        {
            throw new ArgumentOutOfRangeException(nameof(namePolish));
        }

        if (namePolishOld is not null && string.IsNullOrWhiteSpace(namePolishOld))
        {
            throw new ArgumentOutOfRangeException(nameof(namePolishOld));
        }

        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.NamePolish = namePolish;
        this.NamePolishOld = namePolishOld;
        this.Location = location;
    }

    public int StationId { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NamePolishOld { get; private set; }

    public Location? Location { get; private set; }

    public IReadOnlyCollection<SegmentExtreme> SegmentExtremes => this.segmentExtremes.AsReadOnly();
}