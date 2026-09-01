// <copyright file="BasePolymorphicQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

using System.Text.Json.Serialization;

using TrainDude.Features.Lines.GetLine;
using TrainDude.Features.Lines.GetLines;
using TrainDude.Features.Radii.GetRadii;
using TrainDude.Features.Segments.GetSegment;
using TrainDude.Features.Segments.GetSegments;
using TrainDude.Features.Settings.GetNameMode;
using TrainDude.Features.Stations.GetStation;
using TrainDude.Features.Stations.GetStations;
using TrainDude.Features.Trips.GetTrip;
using TrainDude.Features.Trips.GetTrips;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(GetLinesQuery), nameof(GetLinesQuery))]
[JsonDerivedType(typeof(GetLineQuery), nameof(GetLineQuery))]
[JsonDerivedType(typeof(GetRadiiQuery), nameof(GetRadiiQuery))]
[JsonDerivedType(typeof(GetSegmentQuery), nameof(GetSegmentQuery))]
[JsonDerivedType(typeof(GetSegmentsQuery), nameof(GetSegmentsQuery))]
[JsonDerivedType(typeof(GetNameModeQuery), nameof(GetNameModeQuery))]
[JsonDerivedType(typeof(GetStationsQuery), nameof(GetStationsQuery))]
[JsonDerivedType(typeof(GetStationQuery), nameof(GetStationQuery))]
[JsonDerivedType(typeof(GetTripQuery), nameof(GetTripQuery))]
[JsonDerivedType(typeof(GetTripsQuery), nameof(GetTripsQuery))]
public abstract record BasePolymorphicQuery<TResult>;