// <copyright file="GetRoutesQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Network.DTOs;
using TrainDude.Network.Extensions;
using TrainDude.Network.Queries;

internal class GetRoutesQueryHandler : IRequestHandler<GetSegmentsQuery, IEnumerable<SegmentSummaryDTO>>
{
    private readonly NetworkDbContext db;

    public GetRoutesQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<SegmentSummaryDTO>> Handle(GetSegmentsQuery request, CancellationToken cancellationToken)
    {
        var nameMap = (await this.db.Stations.AsNoTracking().ToListAsync(cancellationToken)).ToDictionary(x => x.StationId, x => x);

        var models = await this.db.Segments.AsNoTracking()
            .Select(x => new
            {
                x.SegmentId,
                x.NominalLength,
                A = new
                {
                    Name = x.Extremes.Where(y => !y.IsEnd).Select(y => y.Station.NameGermanNew ?? y.Station.NameGerman).Single(),
                    Location = x.Extremes.Where(y => !y.IsEnd).Select(y => y.Station.Location).Single(),
                },
                B = new
                {
                    Name = x.Extremes.Where(y => y.IsEnd).Select(y => y.Station.NameGermanNew ?? y.Station.NameGerman).Single(),
                    Location = x.Extremes.Where(y => y.IsEnd).Select(y => y.Station.Location).Single(),
                },
            })
            .ToListAsync(cancellationToken);

        var dtos = models
            .Select(x => new SegmentSummaryDTO
            {
                SegmentId = x.SegmentId,
                Length = x.NominalLength,
                NameA = x.A.Name,
                NameB = x.B.Name,
                Haversine = (x.A.Location != null && x.B.Location != null) ? new[] { x.A.Location, x.B.Location }.Segments().Haversine() : null,
            })
            .ToList();

        return dtos;
    }
}