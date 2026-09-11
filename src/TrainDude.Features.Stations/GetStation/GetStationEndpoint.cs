// <copyright file="GetStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStation;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Stations.Contracts.GetStation;
using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;
using Wolverine.Http.Marten;
using Wolverine.Persistence.EventSourcing;

public static class GetStationEndpoint
{
    [WolverineGet(GetStationQuery.TypeRoute)]
    [Tags("Stations")]
    public static GetStationQueryResult Handle([AsParameters] GetStationQuery query, [Document(FromRoute = "id")] StationReadModel readModel)
    {
        var result = new GetStationQueryResult(readModel.Name, readModel.Location, readModel.AxleCount);

        return result;
    }
}