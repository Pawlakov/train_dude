// <copyright file="GetSegmentAntiradiusSeriesQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

public class GetSegmentAntiradiusSeriesQuery
    : IRequest<string>
{
    public int SegmentId { get; }

    public int Resolution { get; }

    public GetSegmentAntiradiusSeriesQuery(int segmentId, int resolution)
    {
        this.SegmentId = segmentId;
        this.Resolution = resolution;
    }
}