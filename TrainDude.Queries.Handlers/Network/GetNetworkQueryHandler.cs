// <copyright file="GetNetworkQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Network;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Mediator;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Contracts.Network;

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
            .Where(x => x.A.Location != null && x.B.Location != null)
            .Select(x => new
            {
                AStationId = x.A.StationId,
                ALocation = x.A.Location!.Value,
                BStationId = x.B.StationId,
                BLocation = x.B.Location!.Value,
                x.Vertices,
            })
            .ToList();

        return new GetNetworkQueryResult
        {
            StationPoints = stations,
            SegmentLineStrings = segments
                .Select(x => (x.Vertices ?? []).Prepend(x.ALocation).Prepend(x.BLocation).ToList())
                .ToList(),
        };
    }
}