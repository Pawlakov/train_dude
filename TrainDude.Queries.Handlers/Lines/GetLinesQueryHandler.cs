// <copyright file="GetLinesQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Lines;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Requests.Lines;

public sealed class GetLinesQueryHandler
    : IQueryHandler<GetLinesQuery, GetLinesQueryResult>
{
    private readonly ILiteCollection<Line> lineRepository;

    public GetLinesQueryHandler(ILiteCollection<Line> lineRepository)
    {
        this.lineRepository = lineRepository;
    }

    public ValueTask<GetLinesQueryResult> Handle(GetLinesQuery request, CancellationToken cancellationToken)
    {
        var models = this.lineRepository.Query().ToList();

        var items = models
            .Select(x => new GetLinesQueryResultItem { LineId = x.Id, LineDesignation = x.LineDesignation })
            .ToList();

        return ValueTask.FromResult(new GetLinesQueryResult { Items = items });
    }
}