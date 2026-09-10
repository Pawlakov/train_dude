// <copyright file="GetTripsEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTrips;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Trips.Contracts.GetTrips;
using TrainDude.Features.Trips.Domain;

using Wolverine.Http;

public static class GetTripsEndpoint
{
    [WolverineGet(GetTripsQuery.TypeRoute)]
    [Tags("Trips")]
    public static async Task<GetTripsQueryResult> Handle([AsParameters] GetTripsQuery request, IQuerySession session, CancellationToken cancellationToken)
    {
        var trips = await session.Query<TripAggregate>()
            .ToListAsync(cancellationToken);

        var items = trips
            .OrderBy(x => x.TripNumber)
            .Select(x => new GetTripsQueryResultItem { TripId = x.Id, TripNumber = x.TripNumber })
            .ToList();

        return new GetTripsQueryResult(items);
    }
}