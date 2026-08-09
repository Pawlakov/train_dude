// <copyright file="GetStationQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Queries.Handlers.GetStationQuery;

using System;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Requests.GetStationQuery;

public sealed class GetStationQueryHandler
    : IQueryHandler<GetStationQuery, GetStationQueryResult>
{
    private readonly ILiteCollection<Station> stationRepository;

    public GetStationQueryHandler(ILiteCollection<Station> stationRepository)
    {
        this.stationRepository = stationRepository;
    }

    public ValueTask<GetStationQueryResult> Handle(GetStationQuery query, CancellationToken cancellationToken)
    {
        var queryResult = this.stationRepository.FindById(query.StationId);
        if (queryResult == null)
        {
            throw new ApplicationException("No aggregate with this ID. If this exception is thrown it means that validation has failed.");
        }

        var result = new GetStationQueryResult
        {
            Name = queryResult.NamePolish ?? queryResult.NameRussian ?? "???",
        };

        return ValueTask.FromResult(result);
    }
}