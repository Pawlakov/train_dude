// <copyright file="GetSegmentsEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.Projections;
using TrainDude.Features.Segments.ReadModels;

using Wolverine;
using Wolverine.Http;

public static class GetSegmentsEndpoint
{
    public const string Route = "/segments";

    [WolverineGet(Route)]
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
                A = new GetSegmentsQueryResultItem.SegmentEnd { Id = x.A.Id, Name = x.A.Name },
                B = new GetSegmentsQueryResultItem.SegmentEnd { Id = x.B.Id, Name = x.B.Name },
            })
            .ToList();

        return new GetSegmentsQueryResult { Items = items };
    }
}