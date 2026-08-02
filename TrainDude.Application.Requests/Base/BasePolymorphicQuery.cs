// <copyright file="BasePolymorphicQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.Base;

using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Application.Requests.GetNetworkGeoJsonQuery;
using TrainDude.Application.Requests.GetRadiiQuery;
using TrainDude.Application.Requests.GetSegmentGeoJsonQuery;
using TrainDude.Application.Requests.GetSegmentQuery;
using TrainDude.Application.Requests.GetSegmentsQuery;
using TrainDude.Application.Requests.GetStationsQuery;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(GetRadiiQuery), nameof(GetRadiiQuery))]
[JsonDerivedType(typeof(GetStationsQuery), nameof(GetStationsQuery))]
[JsonDerivedType(typeof(GetNetworkGeoDataQuery), nameof(GetNetworkGeoDataQuery))]
[JsonDerivedType(typeof(GetSegmentGeoDataQuery), nameof(GetSegmentGeoDataQuery))]
[JsonDerivedType(typeof(GetSegmentQuery), nameof(GetSegmentQuery))]
[JsonDerivedType(typeof(GetSegmentsQuery), nameof(GetSegmentsQuery))]
public abstract record class BasePolymorphicQuery
    : IMessage
{
}