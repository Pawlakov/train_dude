// <copyright file="GetRadiiEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.GetRadii;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Radii.Contracts.GetRadii;
using TrainDude.Features.Radii.Domain;

using Wolverine.Http;

public static class GetRadiiEndpoint
{
    [WolverinePost(GetRadiiQuery.TypeRoute)]
    public static async Task<GetRadiiQueryResult> Handle(GetRadiiQuery query, IQuerySession session, CancellationToken cancellationToken)
    {
        var radii = await session.Query<RadiusAggregate>()
            .OrderBy(x => x.Speed)
            .ToListAsync(cancellationToken);

        var items = radii
            .Select(x => new GetRadiiQueryResultItem(x.Id, x.Speed, x.Minimum, 1000 / (double)x.Minimum))
            .ToList();

        return new GetRadiiQueryResult(items);
    }
}