// <copyright file="BasePolymorphicQueryResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Text.Json.Serialization;

using TrainDude.Queries.Requests.GetNetworkGeoJsonQuery;
using TrainDude.Queries.Requests.GetRadiiQuery;
using TrainDude.Queries.Requests.GetSegmentQuery;
using TrainDude.Queries.Requests.GetSegmentsQuery;
using TrainDude.Queries.Requests.GetStationsQuery;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(GetRadiiQueryResult), nameof(GetRadiiQueryResult))]
[JsonDerivedType(typeof(GetStationsQueryResult), nameof(GetStationsQueryResult))]
[JsonDerivedType(typeof(GetNetworkQueryResult), nameof(GetNetworkQueryResult))]
[JsonDerivedType(typeof(GetSegmentQueryResult), nameof(GetSegmentQueryResult))]
[JsonDerivedType(typeof(GetSegmentsQueryResult), nameof(GetSegmentsQueryResult))]
public abstract class BasePolymorphicQueryResponse
{
}