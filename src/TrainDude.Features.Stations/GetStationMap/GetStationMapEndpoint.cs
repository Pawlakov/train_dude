// <copyright file="GetStationMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStationMap;

using System.Linq;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.GetStationMap;
using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetStationMapEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetStationMapQuery.TypeRoute)]
    public static MapQueryResult Handle(GetStationMapQuery query, StationReadModel readModel)
    {
        var result = new MapQueryResult(new[] { readModel.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(), []);

        return result;
    }
}