// <copyright file="GetRadiiQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Radii;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetRadiiQuery
    : BaseEntityListQuery<GetRadiiQueryResult>
{
}