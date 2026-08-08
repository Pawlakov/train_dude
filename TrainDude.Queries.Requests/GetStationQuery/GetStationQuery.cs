// <copyright file="GetStationQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetStationQuery;

using System;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetStationQuery
    : BasePolymorphicQuery, IQuery<GetStationQueryResult>
{
    public Guid StationId { get; set; }
}