// <copyright file="GetNetworkQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Network;

using Mediator;

using TrainDude.Queries.Contracts.Base;

/// <summary>
/// A query which returns all stations and routes in the form of GeoJSON.
/// </summary>
public sealed record class GetNetworkQuery
    : BasePolymorphicQuery, IQuery<GetNetworkQueryResult>
{
}