// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentQuery;

using MediatR;

using TrainDude.Application.Requests.Base;

public class GetSegmentQuery
    : BaseClientRequest, IRequest<GetSegmentQueryResult?>
{
    public int SegmentId { get; init; }
}