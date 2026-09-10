// <copyright file="GetSegmentsMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Segments.GetSegmentsMap;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using TrainDude.Features.Segments.Contracts.GetSegmentsMap;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;

public static class GetSegmentsMapEndpoint
{
    [WolverineGet(GetSegmentsMapQuery.TypeRoute)]
    [AllowAnonymous]
    [Tags("Segments")]
    public static async Task<MapQueryResult> Handle([AsParameters] GetSegmentsMapQuery request, IQuerySession session, CancellationToken cancellationToken = default)
    {
        var queryResult = await session.Query<SegmentReadModel>()
            .ToListAsync(cancellationToken);

        var segmentLineStrings = queryResult
            .Where(x => x.A.Location.HasValue && x.B.Location.HasValue)
            .Select(x => (x.Course ?? []).Prepend(x.A.Location!.Value).Append(x.B.Location!.Value).ToList())
            .ToList();

        var result = new MapQueryResult([], segmentLineStrings);

        return result;
    }
}