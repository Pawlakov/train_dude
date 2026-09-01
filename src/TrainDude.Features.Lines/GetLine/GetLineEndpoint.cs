// <copyright file="GetLineEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLine;

using System;
using System.Linq;

using TrainDude.Features.Lines.ReadModels;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetLineEndpoint
{
    public const string Route = "/line";

    [AggregateHandler]
    [WolverineGet(Route)]
    public static GetLineQueryResult Handle(GetLineQuery query, LineReadModel readModel)
    {
        var result = new GetLineQueryResult
        {
            LineDesignation = readModel.LineDesignation,
            Trips = readModel.Trips.Select(x => new GetLineQueryResultTripItem { TripId = x.Id, TripNumber = x.Number }).ToList(),
            Stations = readModel.Stations.Select(x => new GetLineQueryResultStationItem { StationId = x.Id, Name = x.Name }).ToList(),
            StationPoints = readModel.Stations.Where(x => x.Location.HasValue).Select(x => x.Location!.Value).ToList(),
            SegmentLineStrings = readModel.Segments.Where(x => x.FullCourse != null).Select(x => x.FullCourse).ToList(),
        };

        return result;
    }
}