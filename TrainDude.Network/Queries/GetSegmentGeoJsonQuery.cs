// <copyright file="GetSegmentGeoJsonQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

public class GetSegmentGeoJsonQuery : IRequest<string>
{
    public int SegmentId { get; }

    public GetSegmentGeoJsonQuery(int id)
    {
        this.SegmentId = id;
    }
}