// <copyright file="GetStationsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Stations;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Requests.Stations;

public sealed class GetStationsQueryHandler
    : IQueryHandler<GetStationsQuery, GetStationsQueryResult>
{
    private readonly ILiteCollection<Station> stationRepository;

    public GetStationsQueryHandler(ILiteCollection<Station> stationRepository)
    {
        this.stationRepository = stationRepository;
    }

    public ValueTask<GetStationsQueryResult> Handle(GetStationsQuery request, CancellationToken cancellationToken)
    {
        var queryResult = this.stationRepository.FindAll();

        var dtos = queryResult
            .Select(x => new GetStationsQueryResultItem { StationId = x.StationId, Name = x.Name, HasLocation = x.Location != null })
            .ToList();

        return ValueTask.FromResult(new GetStationsQueryResult { Items = dtos });
    }
}