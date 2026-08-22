// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Segments;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetSegmentQuery
    : BaseEntityLookupQuery<GetSegmentQueryResult>
{
}