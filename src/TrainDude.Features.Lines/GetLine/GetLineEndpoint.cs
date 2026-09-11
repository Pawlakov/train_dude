// <copyright file="GetLineEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLine;

using System.Linq;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Lines.Contracts.GetLine;
using TrainDude.Features.Lines.ReadModels;

using Wolverine.Http;
using Wolverine.Http.Marten;

public static class GetLineEndpoint
{
    [WolverineGet(GetLineQuery.TypeRoute)]
    [Tags("Lines")]
    public static GetLineQueryResult Handle([AsParameters] GetLineQuery query, [Document(FromRoute = "id")] LineReadModel readModel)
    {
        var trips = readModel.Trips.Select(x => new GetLineQueryResultTripItem { TripId = x.Id, TripNumber = x.Number }).ToList();
        var stations = readModel.Stations.Select(x => new GetLineQueryResultStationItem { StationId = x.Id, Name = x.Name }).ToList();

        var result = new GetLineQueryResult(readModel.LineDesignation, stations, trips);
        return result;
    }
}