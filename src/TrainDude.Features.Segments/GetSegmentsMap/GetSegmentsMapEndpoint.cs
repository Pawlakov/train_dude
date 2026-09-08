// <copyright file="GetSegmentsMapEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Segments.GetSegmentsMap;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Segments.Contracts.GetSegmentsMap;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;

public static class GetSegmentsMapEndpoint
{
    [WolverinePost(GetSegmentsMapQuery.TypeRoute)]
    public static async Task<MapQueryResult> Handle(GetSegmentsMapQuery request, IQuerySession session, CancellationToken cancellationToken = default)
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