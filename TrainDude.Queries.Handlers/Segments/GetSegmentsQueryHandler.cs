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
using TrainDude.Queries.Handlers.Extensions;
using TrainDude.Queries.Requests.Segments;

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
                x.SegmentId,
                x.NominalLength,
                AStationId = x.A.StationId,
                ALocation = x.A.Location,
                AName = x.A.Name,
                BStationId = x.B.StationId,
                BLocation = x.B.Location,
                BName = x.B.Name,
                x.Vertices,
            })
            .ToList();

        var dtos = models
            .Select(x => new GetSegmentsQueryResultItem
            {
                SegmentId = x.SegmentId,
                Length = x.NominalLength,
                AName = x.AName,
                BName = x.BName,
                Haversine = (x.ALocation.HasValue && x.BLocation.HasValue) ? (x.Vertices ?? []).Prepend(x.ALocation.Value).Append(x.BLocation.Value).ToList().Segments().Haversine() : null,
            })
            .ToList();

        return ValueTask.FromResult(new GetSegmentsQueryResult { Items = dtos });
    }
}