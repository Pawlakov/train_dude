// <copyright file="GetLinesQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Lines;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetLinesQuery
    : BaseEntityListQuery<GetLinesQueryResult>
{
}