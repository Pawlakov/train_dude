// <copyright file="GetRadiiQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetRadiiQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Aggregates;
using TrainDude.Queries.Requests.GetRadiiQuery;

public sealed class GetRadiiQueryHandler
    : IQueryHandler<GetRadiiQuery, GetRadiiQueryResult>
{
    private readonly ILiteCollection<Radius> radiusRepository;

    public GetRadiiQueryHandler(ILiteCollection<Radius> radiusRepository)
    {
        this.radiusRepository = radiusRepository;
    }

    public ValueTask<GetRadiiQueryResult> Handle(GetRadiiQuery request, CancellationToken cancellationToken)
    {
        var models = this.radiusRepository
            .FindAll()
            .ToList();

        var dtos = models
            .Select(x => new GetRadiiQueryResultItem
            {
                RadiusId = x.RadiusId,
                Speed = x.Speed,
                Minimum = x.Minimum,
                MaximumAntiradius = 1000 / (double)x.Minimum,
            })
            .ToList();

        return ValueTask.FromResult(new GetRadiiQueryResult { Items = dtos });
    }
}