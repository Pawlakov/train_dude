// <copyright file="GetNetworkGeoJsonQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

/// <summary>
/// A query which returns all stations and routes in the form of GeoJSON.
/// </summary>
public class GetNetworkGeoJsonQuery : IRequest<string>
{
}