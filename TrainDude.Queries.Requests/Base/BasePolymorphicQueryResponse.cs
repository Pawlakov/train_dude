// <copyright file="BasePolymorphicQueryResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Text.Json.Serialization;

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
[JsonDerivedType(typeof(GetRadiiQueryResult), nameof(GetRadiiQueryResult))]
[JsonDerivedType(typeof(GetStationsQueryResult), nameof(GetStationsQueryResult))]
[JsonDerivedType(typeof(GetStationQueryResult), nameof(GetStationQueryResult))]
[JsonDerivedType(typeof(GetNetworkQueryResult), nameof(GetNetworkQueryResult))]
[JsonDerivedType(typeof(GetSegmentQueryResult), nameof(GetSegmentQueryResult))]
[JsonDerivedType(typeof(GetSegmentsQueryResult), nameof(GetSegmentsQueryResult))]
[JsonDerivedType(typeof(GetLinesQueryResult), nameof(GetLinesQueryResult))]
[JsonDerivedType(typeof(GetLineQueryResult), nameof(GetLineQueryResult))]
[JsonDerivedType(typeof(GetTripsQueryResult), nameof(GetTripsQueryResult))]
[JsonDerivedType(typeof(GetTripQueryResult), nameof(GetTripQueryResult))]
public abstract class BasePolymorphicQueryResponse
{
}