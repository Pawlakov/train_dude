// <copyright file="GetTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTrip;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Trips.Contracts.GetTrip;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Persistence.EventSourcing;

public static class GetTripEndpoint
{
    [WolverineGet(GetTripQuery.TypeRoute)]
    [Tags("Trips")]
    public static GetTripQueryResult Handle([AsParameters] GetTripQuery query, [ReadModel(FromRoute = "0")] TripAggregate aggregate)
    {
        var result = new GetTripQueryResult(aggregate.TripNumber);

        return result;
    }
}