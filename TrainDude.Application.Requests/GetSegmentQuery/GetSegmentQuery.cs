// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentQuery;

using Mediator;

using TrainDude.Application.Requests.Base;

public sealed record class GetSegmentQuery
    : BasePolymorphicQuery, IQuery<GetSegmentQueryResult?>
{
    public int SegmentId { get; init; }
}