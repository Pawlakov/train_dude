// <copyright file="GetTripQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Trips;

using System;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetTripQuery
    : BaseEntityLookupQuery<GetTripQueryResult>
{
}