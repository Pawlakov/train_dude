// <copyright file="GetNetworkQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetNetworkQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Requests.GetNetworkQuery;

public sealed class GetNetworkQueryHandler
    : IQueryHandler<GetNetworkQuery, GetNetworkQueryResult>
{
    private readonly ILiteCollection<Segment> segmentRepository;
    private readonly ILiteCollection<Station> stationRepository;

    public GetNetworkQueryHandler(ILiteCollection<Segment> segmentRepository, ILiteCollection<Station> stationRepository)
    {
        this.segmentRepository = segmentRepository;
        this.stationRepository = stationRepository;
    }

    public async ValueTask<GetNetworkQueryResult> Handle(GetNetworkQuery request, CancellationToken cancellationToken)
    {
        var stations = this.stationRepository.Query()
            .Where(x => x.Location != null)
            .Select(x => x.Location!.Value)
            .ToList();

        var segments = this.segmentRepository
            .Query()
            .Where(x => x.ALocation.HasValue && x.BLocation.HasValue)
            .Select(x => new
            {
                x.AStationId,
                ALocation = x.ALocation!.Value,
                x.BStationId,
                BLocation = x.BLocation!.Value,
                x.Vertices,
            })
            .ToList();

        return new GetNetworkQueryResult
        {
            Stations = stations,
            Segments = segments
                .Select(x => new GetNetworkQueryResultSegmentItem
                {
                    ALocation = x.ALocation,
                    BLocation = x.BLocation,
                    Vertices = x.Vertices?.ToList() ?? [],
                })
                .ToList(),
        };
    }
}