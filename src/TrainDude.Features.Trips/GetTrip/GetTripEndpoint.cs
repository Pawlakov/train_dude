// <copyright file="GetTripEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTrip;

using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Trips.Contracts.GetTrip;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class GetTripEndpoint
{
    [AggregateHandler]
    [WolverinePost(GetTripQuery.TypeRoute)]
    public static GetTripQueryResult Handle(GetTripQuery query, TripAggregate aggregate)
    {
        var result = new GetTripQueryResult(aggregate.TripNumber, [], []); // TODO faktyczne stacje i odcinki

        return result;
    }
}