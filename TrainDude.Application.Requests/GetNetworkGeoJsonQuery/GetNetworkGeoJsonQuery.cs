// <copyright file="GetNetworkGeoJsonQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetNetworkGeoJsonQuery;

using Mediator;

using TrainDude.Application.Requests.Base;

/// <summary>
/// A query which returns all stations and routes in the form of GeoJSON.
/// </summary>
public sealed record class GetNetworkGeoJsonQuery
    : BasePolymorphicQuery, IQuery<GetNetworkGeoJsonQueryResult>
{
}