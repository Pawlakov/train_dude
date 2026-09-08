// <copyright file="GetLineMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLineMap;

using System.Linq;

using TrainDude.Features.Lines.Contracts.GetLineMap;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetLineMapEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetLineMapQuery.TypeRoute)]
    public static MapQueryResult Handle(GetLineMapQuery query, LineReadModel readModel)
    {
        var stationPoints = readModel.Stations.Where(x => x.Location.HasValue).Select(x => x.Location!.Value).ToList();
        var segmentLineStrings = readModel.Segments.Where(x => x.FullCourse != null).Select(x => x.FullCourse).ToList();

        var result = new MapQueryResult(stationPoints, segmentLineStrings);
        return result;
    }
}