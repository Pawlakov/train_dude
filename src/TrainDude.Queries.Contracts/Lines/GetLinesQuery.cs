// <copyright file="GetLinesQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Lines;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetLinesQuery
    : BaseEntityListQuery<GetLinesQueryResult>
{
}