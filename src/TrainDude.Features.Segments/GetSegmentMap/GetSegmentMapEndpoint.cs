// <copyright file="GetSegmentMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegmentMap;

using System.Linq;

using TrainDude.Features.Segments.Contracts.GetSegmentMap;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetSegmentMapEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetSegmentMapQuery.TypeRoute)]
    public static MapQueryResult Handle(GetSegmentMapQuery query, SegmentReadModel readModel)
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
        var result = new MapQueryResult(stationPoints, [course]);

        return result;
    }
}