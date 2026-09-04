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
    [WolverineGet(GetSegmentQuery.TypeRoute)]
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

        var a = new GetSegmentQueryResult.SegmentEnd(readModel.A.Id, readModel.A.Name);
        var b = new GetSegmentQueryResult.SegmentEnd(readModel.B.Id, readModel.B.Name);
        var stationPoints = new[] { readModel.A.Location, readModel.B.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList();
        var result = new GetSegmentQueryResult(
        readModel.Tracks,
        readModel.NominalLength,
        readModel.Haversine,
        a,
        b,
        [],
        stationPoints,
        [course]);

        return result;
    }
}