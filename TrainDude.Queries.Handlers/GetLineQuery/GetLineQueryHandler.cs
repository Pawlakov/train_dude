// <copyright file="GetLineQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetLineQuery;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Requests.GetLineQuery;

public sealed class GetLineQueryHandler
    : IQueryHandler<GetLineQuery, GetLineQueryResult>
{
    private readonly ILiteCollection<Line> lineRepository;

    public GetLineQueryHandler(ILiteCollection<Line> lineRepository)
    {
        this.lineRepository = lineRepository;
    }

    public ValueTask<GetLineQueryResult> Handle(GetLineQuery query, CancellationToken cancellationToken)
    {
        var queryResult = this.lineRepository.FindById(query.LineId);
        if (queryResult == null)
        {
            throw new ApplicationException("No aggregate with this ID. If this exception is thrown it means that validation has failed.");
        }

        var result = new GetLineQueryResult
        {
            LineDesignation = queryResult.LineDesignation,
            Trips = queryResult.Trips.Select(x => new GetLineQueryResultTripItem { TripId = x.TripId, TripNumber = x.TripNumber }).ToList(),
        };

        return ValueTask.FromResult(result);
    }
}