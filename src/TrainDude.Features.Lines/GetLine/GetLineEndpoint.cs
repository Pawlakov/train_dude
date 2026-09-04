// <copyright file="GetLineEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLine;

using System;
using System.Collections.Generic;
using System.Linq;

using TrainDude.Features.Lines.Contracts.GetLine;
using TrainDude.Features.Lines.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetLineEndpoint
{
    [AggregateHandler]
    [WolverineGet(GetLineQuery.TypeRoute)]
    public static GetLineQueryResult Handle(GetLineQuery query, LineReadModel readModel)
    {
        var trips = readModel.Trips.Select(x => new GetLineQueryResultTripItem { TripId = x.Id, TripNumber = x.Number }).ToList();
        var stations = readModel.Stations.Select(x => new GetLineQueryResultStationItem { StationId = x.Id, Name = x.Name }).ToList();
        var stationPoints = readModel.Stations.Where(x => x.Location.HasValue).Select(x => x.Location!.Value).ToList();
        var segmentLineStrings = readModel.Segments.Where(x => x.FullCourse != null).Select(x => x.FullCourse).ToList();

        var result = new GetLineQueryResult(readModel.LineDesignation, stations, trips, stationPoints, segmentLineStrings);
        return result;
    }
}