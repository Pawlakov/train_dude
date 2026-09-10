// <copyright file="GetStationMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStationMap;

using System.Linq;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.GetStationMap;
using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;
using Wolverine.Persistence.EventSourcing;

public static class GetStationMapEndpoint
{
    [WolverineGet(GetStationMapQuery.TypeRoute)]
    [Tags("Stations")]
    public static MapQueryResult Handle(GetStationMapQuery query, [ReadModel(FromRoute = "id")] StationReadModel readModel)
    {
        var result = new MapQueryResult(new[] { readModel.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(), []);

        return result;
    }
}