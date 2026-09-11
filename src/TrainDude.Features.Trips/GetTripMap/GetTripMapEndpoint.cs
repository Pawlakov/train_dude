// <copyright file="GetTripMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTripMap;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Trips.Contracts.GetTripMap;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Persistence.EventSourcing;

public static class GetTripMapEndpoint
{
    [WolverineGet(GetTripMapQuery.TypeRoute)]
    [Tags("Trips")]
    public static MapQueryResult Handle([AsParameters] GetTripMapQuery query, [ReadModel(FromRoute = "id")] TripAggregate aggregate)
    {
        var result = new MapQueryResult([], []); // TODO faktyczne stacje i odcinki

        return result;
    }
}