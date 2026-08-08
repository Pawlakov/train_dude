// <copyright file="BasePolymorphicQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Queries.Requests.GetLineQuery;
using TrainDude.Queries.Requests.GetLinesQuery;
using TrainDude.Queries.Requests.GetNetworkQuery;
using TrainDude.Queries.Requests.GetRadiiQuery;
using TrainDude.Queries.Requests.GetSegmentQuery;
using TrainDude.Queries.Requests.GetSegmentsQuery;
using TrainDude.Queries.Requests.GetStationQuery;
using TrainDude.Queries.Requests.GetStationsQuery;
using TrainDude.Queries.Requests.GetTripQuery;
using TrainDude.Queries.Requests.GetTripsQuery;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(GetRadiiQuery), nameof(GetRadiiQuery))]
[JsonDerivedType(typeof(GetStationsQuery), nameof(GetStationsQuery))]
[JsonDerivedType(typeof(GetStationQuery), nameof(Requests.GetStationQuery))]
[JsonDerivedType(typeof(GetNetworkQuery), nameof(GetNetworkQuery))]
[JsonDerivedType(typeof(GetSegmentQuery), nameof(GetSegmentQuery))]
[JsonDerivedType(typeof(GetSegmentsQuery), nameof(GetSegmentsQuery))]
[JsonDerivedType(typeof(GetLinesQuery), nameof(GetLinesQuery))]
[JsonDerivedType(typeof(GetLineQuery), nameof(GetLineQuery))]
[JsonDerivedType(typeof(GetTripsQuery), nameof(GetTripsQuery))]
[JsonDerivedType(typeof(GetTripQuery), nameof(GetTripQuery))]
public abstract record class BasePolymorphicQuery
    : IMessage
{
}