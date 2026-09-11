// <copyright file="GetLinesEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLines;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Lines.Contracts.GetLines;
using TrainDude.Features.Lines.ReadModels;

using Wolverine.Http;

public static class GetLinesEndpoint
{
    [WolverineGet(GetLinesQuery.TypeRoute)]
    [Tags("Lines")]
    public static async Task<GetLinesQueryResult> Handle([AsParameters] GetLinesQuery request, IQuerySession session, CancellationToken cancellationToken = default)
    {
        var queryResult = await session.Query<LineReadModel>()
            .ToListAsync(cancellationToken);

        var items = queryResult
            .OrderBy(x => x.LineNumber)
            .ThenBy(x => x.LineLetter)
            .Select(x => new GetLinesQueryResultItem
            {
                LineId = x.Id,
                LineDesignation = x.LineDesignation,
            })
            .ToList();

        return new GetLinesQueryResult(items);
    }
}