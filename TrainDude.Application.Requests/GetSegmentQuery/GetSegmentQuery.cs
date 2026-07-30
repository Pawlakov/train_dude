// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentQuery;

using MediatR;

public class GetSegmentQuery : IRequest<GetSegmentQueryResult?>
{
    public int SegmentId { get; }

    public GetSegmentQuery(int stringId)
    {
        this.SegmentId = stringId;
    }
}