// <copyright file="Assemblies.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Architecture.Tests;

using System.Reflection;

using TrainDude.Infrastructure.Stations.Projections;

internal static class Assemblies
{
    public static Assembly Infrastructure => typeof(StationReadModelProjection).Assembly;
}