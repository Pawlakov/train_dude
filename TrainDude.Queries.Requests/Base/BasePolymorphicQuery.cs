// <copyright file="BasePolymorphicQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Queries.Requests.GetLinesQuery;
using TrainDude.Queries.Requests.GetNetworkQuery;
using TrainDude.Queries.Requests.GetRadiiQuery;
using TrainDude.Queries.Requests.GetSegmentQuery;
using TrainDude.Queries.Requests.GetSegmentsQuery;
using TrainDude.Queries.Requests.GetStationsQuery;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(GetRadiiQuery), nameof(GetRadiiQuery))]
[JsonDerivedType(typeof(GetStationsQuery), nameof(GetStationsQuery))]
[JsonDerivedType(typeof(GetNetworkQuery), nameof(GetNetworkQuery))]
[JsonDerivedType(typeof(GetSegmentQuery), nameof(GetSegmentQuery))]
[JsonDerivedType(typeof(GetSegmentsQuery), nameof(GetSegmentsQuery))]
[JsonDerivedType(typeof(GetLinesQuery), nameof(GetLinesQuery))]
public abstract record class BasePolymorphicQuery
    : IMessage
{
}