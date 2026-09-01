// <copyright file="GetTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTrip;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetTripEndpoint
{
    public const string Route = "/trip";

    [AggregateHandler]
    [WolverineGet(Route)]
    public static GetTripQueryResult Handle(GetTripQuery query, TripAggregate aggregate)
    {
        var result = new GetTripQueryResult
        {
            TripNumber = aggregate.TripNumber,
            StationPoints = [], // TODO
            SegmentLineStrings = [], // TODO
        };

        return result;
    }
}