// <copyright file="GetTripsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetTripsQuery;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetTripsQuery
    : BasePolymorphicQuery, IQuery<GetTripsQueryResult>
{
}