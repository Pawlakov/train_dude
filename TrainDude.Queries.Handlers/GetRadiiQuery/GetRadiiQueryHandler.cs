// <copyright file="GetRadiiQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetRadiiQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data;
using TrainDude.Queries.Requests.GetRadiiQuery;

public sealed class GetRadiiQueryHandler
    : IQueryHandler<GetRadiiQuery, GetRadiiQueryResult>
{
    private readonly IRadiusRepository db;

    public GetRadiiQueryHandler(IRadiusRepository db)
    {
        this.db = db;
    }

    public async ValueTask<GetRadiiQueryResult> Handle(GetRadiiQuery request, CancellationToken cancellationToken)
    {
        var models = await this.db.RadiusAggregates.AsNoTracking().ToListAsync(cancellationToken);
        var dtos = models
            .Select(x => new GetRadiiQueryResultItem
            {
                RadiusId = x.RadiusId,
                Speed = x.Speed,
                Minimum = x.Minimum,
                MaximumAntiradius = 1000 / (double)x.Minimum,
            })
            .ToList();

        return new GetRadiiQueryResult { Items = dtos };
    }
}