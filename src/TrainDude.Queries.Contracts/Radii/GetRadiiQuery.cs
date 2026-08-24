// <copyright file="GetRadiiQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Radii;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetRadiiQuery
    : BaseEntityListQuery<GetRadiiQueryResult>
{
}