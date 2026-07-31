// <copyright file="BaseClientRequest.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.Base;

using System.Text.Json.Serialization;

using TrainDude.Application.Requests.DropAndSeedCommand;
using TrainDude.Application.Requests.GetNetworkGeoJsonQuery;
using TrainDude.Application.Requests.GetRadiiQuery;
using TrainDude.Application.Requests.GetSegmentGeoJsonQuery;
using TrainDude.Application.Requests.GetSegmentQuery;
using TrainDude.Application.Requests.GetSegmentsQuery;
using TrainDude.Application.Requests.GetStationsQuery;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(DropAndSeedCommand), nameof(DropAndSeedCommand))]
[JsonDerivedType(typeof(GetRadiiQuery), nameof(GetRadiiQuery))]
[JsonDerivedType(typeof(GetStationsQuery), nameof(GetStationsQuery))]
[JsonDerivedType(typeof(GetNetworkGeoJsonQuery), nameof(GetNetworkGeoJsonQuery))]
[JsonDerivedType(typeof(GetSegmentGeoJsonQuery), nameof(GetSegmentGeoJsonQuery))]
[JsonDerivedType(typeof(GetSegmentQuery), nameof(GetSegmentQuery))]
[JsonDerivedType(typeof(GetSegmentsQuery), nameof(GetSegmentsQuery))]
public abstract class BaseClientRequest
{
}