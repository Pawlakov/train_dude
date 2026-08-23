// <copyright file="GetSegmentsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Segments;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Contracts.Segments;

public sealed class GetSegmentsQueryHandler
    : IQueryHandler<GetSegmentsQuery, GetSegmentsQueryResult>
{
    private readonly ILiteCollection<Segment> segmentRepository;
    private readonly ILiteCollection<Station> stationRepository;

    public GetSegmentsQueryHandler(ILiteCollection<Segment> segmentRepository, ILiteCollection<Station> stationRepository)
    {
        this.segmentRepository = segmentRepository;
        this.stationRepository = stationRepository;
    }

    public ValueTask<GetSegmentsQueryResult> Handle(GetSegmentsQuery request, CancellationToken cancellationToken)
    {
        var models = this.segmentRepository.Query()
            .Select(x => new
            {
                x.Id,
                x.NominalLength,
                AName = x.A.Name,
                BName = x.B.Name,
                x.Haversine,
            })
            .ToList();

        var dtos = models
            .Select(x => new GetSegmentsQueryResultItem
            {
                SegmentId = x.Id,
                Length = x.NominalLength,
                AName = x.AName,
                BName = x.BName,
                Haversine = x.Haversine,
            })
            .ToList();

        return ValueTask.FromResult(new GetSegmentsQueryResult { Items = dtos });
    }
}