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

using TrainDude.Queries.Contracts.Segments;
using TrainDude.Queries.Data.Documents;

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

        var course = (queryResult.A.Location, queryResult.B.Location) switch
        {
            ({} aLocation, {} bLocation) => (queryResult.Course ?? [])
                .Prepend(aLocation)
                .Append(bLocation)
                .ToList(),
            _ => [],
        };

        var trips = (queryResult.Trips ?? [])
            .Select(x => new GetSegmentQueryResult.SegmentTrip
            {
                Id = x.TripId,
                Number = x.Number,
            })
            .ToList();

        var dto = new GetSegmentQueryResult
        {
            Tracks = queryResult.Tracks,
            NominalLength = queryResult.NominalLength,
            Haversine = queryResult.Haversine,
            A = new()
            {
                Id = queryResult.A.StationId,
                Name = queryResult.A?.Name ?? string.Empty,
            },
            B = new()
            {
                Id = queryResult.B.StationId,
                Name = queryResult.B?.Name ?? string.Empty,
            },
            Trips = trips,
            StationPoints = new[] { queryResult.A?.Location, queryResult.B?.Location }.Where(x => x.HasValue).Select(x => x.Value).ToList(),
            SegmentLineStrings = [course],
        };

        return ValueTask.FromResult<GetSegmentQueryResult>(dto);
    }
}