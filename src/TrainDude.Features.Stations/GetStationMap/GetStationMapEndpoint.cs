// <copyright file="GetStationMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStationMap;

using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.GetStationMap;
using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;
using Wolverine.Http.Marten;
using Wolverine.Persistence.EventSourcing;

public static class GetStationMapEndpoint
{
    public sealed record GetStationMapRequest([FromRoute] Guid Id);

    [WolverineGet(GetStationMapQuery.TypeRoute)]
    [Tags("Stations")]
    public static MapQueryResponse Handle([AsParameters] GetStationMapRequest request, [Document(FromRoute = "id")] StationReadModel readModel)
    {
        var query = new GetStationMapQuery(request.Id);
        var result = new MapQueryResponse(new[] { readModel.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(), []);

        return result;
    }
}