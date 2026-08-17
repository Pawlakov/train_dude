// <copyright file="BasePolymorphicQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Text.Json.Serialization;

using TrainDude.Queries.Requests.Admin;
using TrainDude.Queries.Requests.Lines;
using TrainDude.Queries.Requests.Network;
using TrainDude.Queries.Requests.Radii;
using TrainDude.Queries.Requests.Segments;
using TrainDude.Queries.Requests.Stations;
using TrainDude.Queries.Requests.Trips;

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
[JsonDerivedType(typeof(GetStationNameModeQueryResult), nameof(GetStationNameModeQueryResult))]
public abstract class BasePolymorphicQueryResult
{
}