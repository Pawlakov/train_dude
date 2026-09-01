// <copyright file="GetRadiiEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.GetRadii;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Radii.Domain;

using Wolverine.Http;

public static class GetRadiiEndpoint
{
    public const string Route = "/radii";

    [WolverineGet(Route)]
    public static async Task<GetRadiiQueryResult> Handle(GetRadiiQuery query, IQuerySession session, CancellationToken cancellationToken)
    {
        var radii = await session.Query<RadiusAggregate>()
            .OrderBy(x => x.Speed)
            .ToListAsync(cancellationToken);

        var items = radii
            .Select(x => new GetRadiiQueryResultItem
            {
                RadiusId = x.Id,
                Speed = x.Speed,
                Minimum = x.Minimum,
                MaximumAntiradius = 1000 / (double)x.Minimum,
            })
            .ToList();

        return new GetRadiiQueryResult { Items = items };
    }
}