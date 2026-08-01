// <copyright file="GetSegmentGeoJsonQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentGeoJsonQuery;

using Mediator;

using TrainDude.Application.Requests.Base;

public sealed record class GetSegmentGeoJsonQuery
    : BasePolymorphicQuery, IQuery<GetSegmentGeoJsonQueryResult>
{
    public int SegmentId { get; init; }
}