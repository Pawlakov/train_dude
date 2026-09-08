// <copyright file="GetSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegment;

using TrainDude.Features.Segments.Contracts.GetSegment;
using TrainDude.Features.Segments.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetSegmentEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetSegmentQuery.TypeRoute)]
    public static GetSegmentQueryResult Handle(GetSegmentQuery query, SegmentReadModel readModel)
    {
        var result = new GetSegmentQueryResult(
        readModel.Tracks,
        readModel.NominalLength,
        readModel.Haversine,
        readModel.A.Id,
        readModel.A.Name,
        readModel.B.Id,
        readModel.B.Name,
        []);

        return result;
    }
}