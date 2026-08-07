// <copyright file="GetStationsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetStationsQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data;
using TrainDude.Queries.Data.Aggregates;
using TrainDude.Queries.Requests.GetStationsQuery;

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
        var models = this.stationRepository
            .Query()
            .Select(x => new
            {
                /*x.StationId,
                Name = x.NameGermanNew ?? x.NameGerman,*/
                x.Location,
            })
            .ToList();

        var dtos = models
            .Select(x => new GetStationsQueryResultItem { /*StationId = x.StationId, Name = x.Name,*/ HasLocation = x.Location != null })
            .ToList();

        return ValueTask.FromResult(new GetStationsQueryResult { Items = dtos });
    }
}