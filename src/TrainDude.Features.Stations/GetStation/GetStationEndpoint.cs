// <copyright file="GetStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStation;

using System.Linq;

using TrainDude.Features.Stations.Contracts.GetStation;
using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetStationEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetStationQuery.TypeRoute)]
    public static GetStationQueryResult Handle(GetStationQuery query, StationReadModel readModel)
    {
        var result = new GetStationQueryResult(readModel.Name, readModel.Location, readModel.AxleCount);

        return result;
    }
}