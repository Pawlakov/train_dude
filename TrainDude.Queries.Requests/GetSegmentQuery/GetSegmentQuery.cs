// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetSegmentQuery;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetSegmentQuery
    : BasePolymorphicQuery, IQuery<GetSegmentQueryResult?>
{
    public int SegmentId { get; set; }
}