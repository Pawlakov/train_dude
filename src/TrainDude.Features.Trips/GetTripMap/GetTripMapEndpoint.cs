// <copyright file="GetTripMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTripMap;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Trips.Contracts.GetTripMap;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetTripMapEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetTripMapQuery.TypeRoute)]
    public static MapQueryResult Handle(GetTripMapQuery query, TripAggregate aggregate)
    {
        var result = new MapQueryResult([], []); // TODO faktyczne stacje i odcinki

        return result;
    }
}