// <copyright file="GetTripQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Trips;

using System;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetTripQuery
    : BaseEntityLookupQuery<GetTripQueryResult>
{
}