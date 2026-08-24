// <copyright file="BasePolymorphicQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Base;

using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Queries.Contracts.Admin;
using TrainDude.Queries.Contracts.Lines;
using TrainDude.Queries.Contracts.Network;
using TrainDude.Queries.Contracts.Radii;
using TrainDude.Queries.Contracts.Segments;
using TrainDude.Queries.Contracts.Stations;
using TrainDude.Queries.Contracts.Trips;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(GetRadiiQuery), nameof(GetRadiiQuery))]
[JsonDerivedType(typeof(GetStationsQuery), nameof(GetStationsQuery))]
[JsonDerivedType(typeof(GetStationQuery), nameof(GetStationQuery))]
[JsonDerivedType(typeof(GetNetworkQuery), nameof(GetNetworkQuery))]
[JsonDerivedType(typeof(GetSegmentQuery), nameof(GetSegmentQuery))]
[JsonDerivedType(typeof(GetSegmentsQuery), nameof(GetSegmentsQuery))]
[JsonDerivedType(typeof(GetLinesQuery), nameof(GetLinesQuery))]
[JsonDerivedType(typeof(GetLineQuery), nameof(GetLineQuery))]
[JsonDerivedType(typeof(GetTripsQuery), nameof(GetTripsQuery))]
[JsonDerivedType(typeof(GetTripQuery), nameof(GetTripQuery))]
[JsonDerivedType(typeof(GetStationNameModeQuery), nameof(GetStationNameModeQuery))]
public abstract record class BasePolymorphicQuery
    : IMessage
{
}