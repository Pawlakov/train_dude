// <copyright file="GetStationQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Stations;

using System;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetStationQuery
    : BaseEntityLookupQuery<GetStationQueryResult>
{
}