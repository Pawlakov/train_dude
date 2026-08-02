// <copyright file="GetNetworkGeoDataQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetNetworkGeoJsonQuery;

using Mediator;

using TrainDude.Application.Requests.Base;

/// <summary>
/// A query which returns all stations and routes in the form of GeoJSON.
/// </summary>
public sealed record class GetNetworkGeoDataQuery
    : BasePolymorphicQuery, IQuery<GetNetworkGeoDataQueryResult>
{
}