// <copyright file="GetSegmentsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Segments;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetSegmentsQuery
    : BaseEntityListQuery<GetSegmentsQueryResult>
{
}