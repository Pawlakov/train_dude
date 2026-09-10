// <copyright file="GetSegmentsEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegments;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.GetSegments;
using TrainDude.Features.Segments.ReadModels;

using Wolverine.Http;

public static class GetSegmentsEndpoint
{
    [WolverineGet(GetSegmentsQuery.TypeRoute)]
    [Tags("Segments")]
    public static async Task<GetSegmentsQueryResult> Handle(GetSegmentsQuery request, IQuerySession session, CancellationToken cancellationToken = default)
    {
        var queryResult = await session.Query<SegmentReadModel>()
            .ToListAsync(cancellationToken);

        var items = queryResult
            .Select(x => new GetSegmentsQueryResultItem
            {
                SegmentId = x.Id,
                Length = x.NominalLength,
                Haversine = x.Haversine,
                AId = x.A.Id,
                AName = x.A.Name,
                BId = x.B.Id,
                BName = x.B.Name,
            })
            .ToList();

        return new GetSegmentsQueryResult(items);
    }
}