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
using TrainDude.Queries.Contracts.Segments;
using TrainDude.Shared.Values;

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

        var vertices = (queryResult.Course ?? [])
            .Cast<Location?>()
            .Prepend(queryResult.A.Location)
            .Append(queryResult.B.Location)
            .Where(x => x != null)
            .Select(x => x.Value)
            .ToList();

        var dto = new GetSegmentQueryResult
        {
            Tracks = queryResult.Tracks,
            NominalLength = queryResult.NominalLength,
            Haversine = queryResult.Haversine,
            AId = queryResult.A.StationId,
            AName = queryResult.A?.Name ?? string.Empty,
            BId = queryResult.B.StationId,
            BName = queryResult.B?.Name ?? string.Empty,
            StationPoints = new[] { queryResult.A?.Location, queryResult.B?.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(),
            SegmentLineStrings = [vertices],
        };

        return ValueTask.FromResult<GetSegmentQueryResult>(dto);
    }
}