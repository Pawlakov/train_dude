// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

using TrainDude.Network.DTOs;

public class GetSegmentQuery : IRequest<SegmentDetailsDTO?>
{
    public int SegmentId { get; }

    public GetSegmentQuery(int stringId)
    {
        this.SegmentId = stringId;
    }
}