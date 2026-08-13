// <copyright file="BasePolymorphicQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Queries.Requests.Admin;
using TrainDude.Queries.Requests.Lines;
using TrainDude.Queries.Requests.Network;
using TrainDude.Queries.Requests.Radii;
using TrainDude.Queries.Requests.Segments;
using TrainDude.Queries.Requests.Stations;
using TrainDude.Queries.Requests.Trips;

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