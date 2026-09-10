// <copyright file="GetSegmentMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegmentMap;

using System.Linq;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.GetSegmentMap;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;
using Wolverine.Persistence.EventSourcing;

public static class GetSegmentMapEndpoint
{
    [WolverineGet(GetSegmentMapQuery.TypeRoute)]
    [Tags("Segments")]
    public static MapQueryResult Handle([AsParameters] GetSegmentMapQuery query, [ReadModel(FromRoute = "0")] SegmentReadModel readModel)
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