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

using TrainDude.Data;
using TrainDude.Data.Models;
using TrainDude.Network.DTOs;
using TrainDude.Network.Extensions;
using TrainDude.Network.Queries;

internal class GetSegmentsQueryHandler : IRequestHandler<GetSegmentsQuery, IEnumerable<SegmentSummaryDTO>>
{
    private readonly NetworkDbContext db;

    public GetSegmentsQueryHandler(NetworkDbContext db)
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
                Vertices = x.Vertices.OrderBy(y => y.OrdinalId).ToList(),
                Charts = x.Charts.Select(y => y.ChartId).ToList(),
            })
            .ToListAsync(cancellationToken);

        var dtos = models
            .Select(x => new SegmentSummaryDTO
            {
                SegmentId = x.SegmentId,
                Length = x.NominalLength,
                NameA = x.A.Name,
                NameB = x.B.Name,
                Haversine = (x.A.Location != null && x.B.Location != null) ? x.Vertices.Cast<Location>().Prepend(x.A.Location).Append(x.A.Location).Segments().Haversine() : null,
                Charts = x.Charts,
            })
            .ToList();

        return dtos;
    }
}