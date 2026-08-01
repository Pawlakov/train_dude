// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetStationsQuery;

using Mediator;

using TrainDude.Application.Requests.Base;

/// <summary>
/// A query which returns all stations.
/// </summary>
public sealed record class GetStationsQuery
    : BasePolymorphicQuery, IQuery<GetStationsQueryResult>
{
}