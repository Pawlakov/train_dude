// <copyright file="StationNameResolver.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Enums;

public class StationNameResolver
{
    private const string FallbackStationName = "???";

    public static Func<IHasAlternativeNames, string> BuildNameSelector(NamingPolicy policy)
    {
        return station => SelectName(policy, station.NameGerman, station.NameGermanNew, station.NamePolish, station.NameRussian);
    }

    public static Func<IHasAlternativeNames, string> GetNameSelector(NamingPolicy policy)
    {
        return BuildNameSelector(policy);
    }

    private static string SelectName(NamingPolicy mode, string german, string? germanNew, string? polish, string? russian) =>
        mode switch
        {
            NamingPolicy.German => germanNew ?? german,
            _ => polish ?? russian ?? StationNameResolver.FallbackStationName,
        };
}