// <copyright file="GetStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStation;

using System.Linq;

using TrainDude.Features.Stations.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetStationEndpoint
{
    public const string Route = "/station";

    [AggregateHandler]
    [WolverineGet(Route)]
    public static GetStationQueryResult Handle(GetStationQuery query, StationReadModel readModel)
    {
        var result = new GetStationQueryResult
        {
            Name = readModel.Name,
            Location = readModel.Location,
            AxleCount = readModel.AxleCount,
            StationPoints = new[] { readModel.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(),
            SegmentLineStrings = [],
        };

        return result;
    }
}