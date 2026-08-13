// <copyright file="GetStationQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Queries.Handlers.Stations;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Requests.Stations;

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
        var queryResult = this.stationRepository.FindById(query.Id);
        if (queryResult == null)
        {
            throw new ApplicationException("No aggregate with this ID. If this exception is thrown it means that validation has failed.");
        }

        var result = new GetStationQueryResult
        {
            Name = queryResult.NamePolish ?? queryResult.NameRussian ?? "???",
            StationPoints = new[] { queryResult.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(),
            SegmentLineStrings = [],
        };

        return ValueTask.FromResult(result);
    }
}