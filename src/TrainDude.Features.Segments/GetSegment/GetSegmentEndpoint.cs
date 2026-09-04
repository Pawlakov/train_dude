// <copyright file="GetSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegment;

using System;
using System.Linq;

using TrainDude.Features.Segments.Contracts.GetSegment;
using TrainDude.Features.Segments.ReadModels;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class GetSegmentEndpoint
{
    [AggregateHandler]
    [WolverineGet(GetSegmentQuery.TypeRoute)]
    public static GetSegmentQueryResult Handle(GetSegmentQuery query, SegmentReadModel readModel)
    {
        var course = (readModel.A.Location, readModel.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => (readModel.Course ?? [])
                .Prepend(aLocation)
                .Append(bLocation)
                .ToList(),
            _ => [],
        };

        var result = new GetSegmentQueryResult
        {
            Tracks = readModel.Tracks,
            NominalLength = readModel.NominalLength,
            Haversine = readModel.Haversine,
            A = new()
            {
                Id = readModel.A.Id,
                Name = readModel.A.Name,
            },
            B = new()
            {
                Id = readModel.B.Id,
                Name = readModel.B.Name,
            },
            Trips = [],
            StationPoints = new[] { readModel.A.Location, readModel.B.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(),
            SegmentLineStrings = [course],
        };

        return result;
    }
}