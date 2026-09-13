// <copyright file="GetSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegment;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.GetSegment;
using TrainDude.Features.Segments.ReadModels;

using Wolverine.Http;
using Wolverine.Http.Marten;

public static class GetSegmentEndpoint
{
    [WolverineGet(GetSegmentQuery.TypeRoute)]
    [Tags("Segments")]
    public static GetSegmentQueryResponse Handle([AsParameters] GetSegmentQuery query, [Document(FromRoute = "id")] SegmentReadModel readModel)
    {
        var result = new GetSegmentQueryResponse(
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