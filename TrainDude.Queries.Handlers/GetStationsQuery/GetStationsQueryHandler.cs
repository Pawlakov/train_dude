// <copyright file="GetStationsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetStationsQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data;
using TrainDude.Queries.Requests.GetStationsQuery;

public sealed class GetStationsQueryHandler
    : IQueryHandler<GetStationsQuery, GetStationsQueryResult>
{
    private readonly IReadDbContext db;

    public GetStationsQueryHandler(IReadDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<GetStationsQueryResult> Handle(GetStationsQuery request, CancellationToken cancellationToken)
    {
        var models = await this.db.Stations.AsNoTracking()
            .Select(x => new
            {
                Id = x.StationId,
                Name = x.NameGermanNew ?? x.NameGerman,
                x.Location,
            })
            .ToListAsync(cancellationToken);

        var dtos = models
            .Select(x => new GetStationsQueryResultItem { StationId = x.Id, Name = x.Name, HasLocation = x.Location != null })
            .ToList();

        return new GetStationsQueryResult { Items = dtos };
    }
}