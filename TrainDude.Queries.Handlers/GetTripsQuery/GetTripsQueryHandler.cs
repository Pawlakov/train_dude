// <copyright file="GetTripsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetTripsQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Aggregates;
using TrainDude.Queries.Requests.GetTripsQuery;

public sealed class GetTripsQueryHandler
    : IQueryHandler<GetTripsQuery, GetTripsQueryResult>
{
    private readonly ILiteCollection<Trip> tripRepository;

    public GetTripsQueryHandler(ILiteCollection<Trip> tripRepository)
    {
        this.tripRepository = tripRepository;
    }

    public ValueTask<GetTripsQueryResult> Handle(GetTripsQuery request, CancellationToken cancellationToken)
    {
        var models = this.tripRepository.Query().ToList();

        var items = models
            .Select(x => new GetTripsQueryResultItem { TripId = x.TripId, TripNumber = x.TripNumber })
            .ToList();

        return ValueTask.FromResult(new GetTripsQueryResult { Items = items });
    }
}