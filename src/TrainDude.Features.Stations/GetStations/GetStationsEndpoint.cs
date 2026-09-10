// <copyright file="GetStationsEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStations;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared;
using TrainDude.Features.Stations.Contracts.GetStations;
using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Stations.ReadModels;

using Wolverine;
using Wolverine.Http;

public static class GetStationsEndpoint
{
    [WolverineGet(GetStationsQuery.TypeRoute)]
    [Tags("Stations")]
    public static async Task<GetStationsQueryResult> Handle([AsParameters] GetStationsQuery query, IQuerySession session, CancellationToken cancellationToken)
    {
        var queryResult = await session.Query<StationReadModel>()
            .ToListAsync(cancellationToken);

        var items = queryResult
            .OrderBy(x => x.Name)
            .Select(x => new GetStationsQueryResultItem { StationId = x.Id, Name = x.Name, HasLocation = x.Location != null })
            .ToList();

        return new GetStationsQueryResult(items);
    }
}