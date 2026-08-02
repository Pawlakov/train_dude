// <copyright file="GetRadiiQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetRadiiQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Requests.GetRadiiQuery;
using TrainDude.Data;

public sealed class GetRadiiQueryHandler
    : IQueryHandler<GetRadiiQuery, GetRadiiQueryResult>
{
    private readonly INetworkDbContext db;

    public GetRadiiQueryHandler(INetworkDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<GetRadiiQueryResult> Handle(GetRadiiQuery request, CancellationToken cancellationToken)
    {
        var models = await this.db.Radii.AsNoTracking().ToListAsync(cancellationToken);
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