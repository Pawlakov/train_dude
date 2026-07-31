// <copyright file="GetSegmentGeoJsonQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentGeoJsonQuery;

using MediatR;

using TrainDude.Application.Requests.Base;

public class GetSegmentGeoJsonQuery
    : BaseClientRequest, IRequest<GetSegmentGeoJsonQueryResult>
{
    public int SegmentId { get; init; }
}