// <copyright file="GetTripQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Trips;

using System;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Contracts.Trips;

public sealed class GetTripQueryHandler
    : IQueryHandler<GetTripQuery, GetTripQueryResult>
{
    private readonly ILiteCollection<Trip> tripRepository;

    public GetTripQueryHandler(ILiteCollection<Trip> tripRepository)
    {
        this.tripRepository = tripRepository;
    }

    public ValueTask<GetTripQueryResult> Handle(GetTripQuery query, CancellationToken cancellationToken)
    {
        var queryResult = this.tripRepository.FindById(query.Id);
        if (queryResult == null)
        {
            throw new ApplicationException("No aggregate with this ID. If this exception is thrown it means that validation has failed.");
        }

        var result = new GetTripQueryResult
        {
            TripNumber = queryResult.TripNumber,
            StationPoints = [], // TODO
            SegmentLineStrings = [], // TODO
        };

        return ValueTask.FromResult(result);
    }
}