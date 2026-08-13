// <copyright file="GetSegmentQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Segments;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Requests.Segments;

public sealed class GetSegmentQueryHandler
    : IQueryHandler<GetSegmentQuery, GetSegmentQueryResult>
{
    private readonly ILiteCollection<Segment> segmentRepository;

    public GetSegmentQueryHandler(ILiteCollection<Segment> segmentRepository)
    {
        this.segmentRepository = segmentRepository;
    }

    public ValueTask<GetSegmentQueryResult> Handle(GetSegmentQuery request, CancellationToken cancellationToken)
    {
        var queryResult = this.segmentRepository.FindById(request.Id);
        if (queryResult == null)
        {
            throw new ApplicationException("No aggregate with this ID. If this exception is thrown it means that validation has failed.");
        }

        var dto = new GetSegmentQueryResult
        {
            AName = queryResult.AName,
            BName = queryResult.BName,
            StationPoints = new[] { queryResult.ALocation, queryResult.BLocation }.Where(x => x.HasValue).Select(x => x.Value).ToList(),
            SegmentLineStrings = new[] { (queryResult.Vertices ?? []).ToList() },
        };

        return ValueTask.FromResult<GetSegmentQueryResult?>(dto);
    }
}