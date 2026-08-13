// <copyright file="GetStationQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Stations;

using System;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetStationQuery
    : BaseEntityLookupQuery<GetStationQueryResult>
{
}