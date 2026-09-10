// <copyright file="GetStationsMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStationsMap;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.GetStationsMap;
using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;

public static class GetStationsMapEndpoint
{
    [WolverineGet(GetStationsMapQuery.TypeRoute)]
    [AllowAnonymous]
    [Tags("Stations")]
    public static async Task<MapQueryResult> Handle([AsParameters] GetStationsMapQuery query, IQuerySession session, CancellationToken cancellationToken)
    {
        var queryResult = await session.Query<StationReadModel>()
            .ToListAsync(cancellationToken);

        var stationPoints = queryResult
            .Where(x => x.Location.HasValue)
            .Select(x => x.Location.Value)
            .ToList();

        var result = new MapQueryResult(stationPoints, []);

        return result;
    }
}