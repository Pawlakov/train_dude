// <copyright file="GetTripsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Trips;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetTripsQuery
    : BaseEntityListQuery<GetTripsQueryResult>
{
}