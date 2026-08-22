// <copyright file="GetTripsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Trips;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetTripsQuery
    : BaseEntityListQuery<GetTripsQueryResult>
{
}