// <copyright file="GetStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStation;

using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Stations.Contracts.GetStation;
using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;
using Wolverine.Http.Marten;

public static class GetStationEndpoint
{
    public sealed record GetStationRequest([FromRoute] Guid Id);

    [WolverineGet(GetStationQuery.TypeRoute)]
    [Tags("Stations")]
    public static GetStationQueryResponse Handle([AsParameters] GetStationRequest request, [Document(FromRoute = "id")] StationReadModel readModel)
    {
        var query = new GetStationQuery(request.Id);
        var result = new GetStationQueryResponse(readModel.Name, readModel.Location, readModel.AxleCount);

        return result;
    }
}