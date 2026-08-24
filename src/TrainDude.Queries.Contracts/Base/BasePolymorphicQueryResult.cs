// <copyright file="BasePolymorphicQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Base;

using System.Text.Json.Serialization;

using TrainDude.Queries.Contracts.Admin;
using TrainDude.Queries.Contracts.Lines;
using TrainDude.Queries.Contracts.Network;
using TrainDude.Queries.Contracts.Radii;
using TrainDude.Queries.Contracts.Segments;
using TrainDude.Queries.Contracts.Stations;
using TrainDude.Queries.Contracts.Trips;

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