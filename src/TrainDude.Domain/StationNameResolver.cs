// <copyright file="StationNameResolver.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain;

using System;

using TrainDude.Domain.Base;
using TrainDude.Shared.Values;

public class StationNameResolver
{
    private const string FallbackStationName = "???";

    public static Func<IHasAlternativeNames, string> BuildNameSelector(StationNameMode mode)
    {
        return station => SelectName(mode, station.NameGerman, station.NameGermanNew, station.NamePolish, station.NameRussian);
    }

    public static Func<IHasAlternativeNames, string> GetNameSelector(StationNameMode mode)
    {
        return BuildNameSelector(mode);
    }

    private static string SelectName(StationNameMode mode, string german, string? germanNew, string? polish, string? russian) =>
        mode switch
        {
            StationNameMode.German => germanNew ?? german,
            _ => polish ?? russian ?? StationNameResolver.FallbackStationName,
        };
}