// <copyright file="GetSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegment;

using System;
using System.Linq;

using TrainDude.Features.Segments.Contracts.GetSegment;
using TrainDude.Features.Segments.ReadModels;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class GetSegmentEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetSegmentQuery.TypeRoute)]
    public static GetSegmentQueryResult Handle(GetSegmentQuery query, SegmentReadModel readModel)
    {
        var course = (readModel.A.Location, readModel.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => (readModel.Course ?? [])
                .Prepend(aLocation)
                .Append(bLocation)
                .ToList(),
            _ => [],
        };

        var stationPoints = new[] { readModel.A.Location, readModel.B.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList();
        var result = new GetSegmentQueryResult(
        readModel.Tracks,
        readModel.NominalLength,
        readModel.Haversine,
        readModel.A.Id,
        readModel.A.Name,
        readModel.B.Id,
        readModel.B.Name,
        [],
        stationPoints,
        [course]);

        return result;
    }
}