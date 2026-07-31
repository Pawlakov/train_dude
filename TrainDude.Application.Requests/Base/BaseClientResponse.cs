// <copyright file="BaseClientResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.Base;

using System.Text.Json.Serialization;

using TrainDude.Application.Requests.GetNetworkGeoJsonQuery;
using TrainDude.Application.Requests.GetRadiiQuery;
using TrainDude.Application.Requests.GetSegmentGeoJsonQuery;
using TrainDude.Application.Requests.GetSegmentQuery;
using TrainDude.Application.Requests.GetSegmentsQuery;
using TrainDude.Application.Requests.GetStationsQuery;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(GetRadiiQueryResult), nameof(GetRadiiQueryResult))]
[JsonDerivedType(typeof(GetStationsQueryResult), nameof(GetStationsQueryResult))]
[JsonDerivedType(typeof(GetNetworkGeoJsonQueryResult), nameof(GetNetworkGeoJsonQueryResult))]
[JsonDerivedType(typeof(GetSegmentGeoJsonQueryResult), nameof(GetSegmentGeoJsonQueryResult))]
[JsonDerivedType(typeof(GetSegmentQueryResult), nameof(GetSegmentQueryResult))]
[JsonDerivedType(typeof(GetSegmentsQueryResult), nameof(GetSegmentsQueryResult))]
public abstract class BaseClientResponse
{
}