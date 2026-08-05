// <copyright file="DataModelProjector.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Projections;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TrainDude.Commands.Data;
using TrainDude.Queries.Data;
using TrainDude.Queries.Data.Entities;

public sealed class DataModelProjector
{
    private readonly IWriteDbContext source;
    private readonly IReadDbContext target;

    public DataModelProjector(IWriteDbContext source, IReadDbContext target)
    {
        this.source = source;
        this.target = target;
    }

    public async Task RebuildAsync(CancellationToken cancellationToken = default)
    {
        var stations = await this.source.Stations
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var radii = await this.source.Radii
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var segments = await this.source.Segments
            .AsNoTracking()
            .Include(s => s.Extremes)
            .ThenInclude(e => e.Station)
            .ToListAsync(cancellationToken);

        await using var transaction = await this.target.BeginTransactionAsync(cancellationToken);

        await this.target.SegmentAggregates.ExecuteDeleteAsync(cancellationToken);
        await this.target.StationAggregates.ExecuteDeleteAsync(cancellationToken);
        await this.target.RadiusAggregates.ExecuteDeleteAsync(cancellationToken);

        foreach (var station in stations)
        {
            await this.target.StationAggregates.AddAsync(new StationAggregate(station.StationId, station.NameGerman, station.NameGermanNew, station.Location), cancellationToken);
        }

        foreach (var radius in radii)
        {
            await this.target.RadiusAggregates.AddAsync(new RadiusAggregate(radius.RadiusId, radius.Speed, radius.Minimum), cancellationToken);
        }

        foreach (var segment in segments)
        {
            var startStation = segment.Extremes.Single(e => !e.IsEnd).Station;
            var endStation = segment.Extremes.Single(e => e.IsEnd).Station;

            var segmentAggregate = new SegmentAggregate(segment.SegmentId, segment.NominalLength);
            segmentAggregate.SetA(startStation.StationId, startStation.NameGerman, startStation.NameGermanNew, startStation.Location);
            segmentAggregate.SetB(endStation.StationId, endStation.NameGerman, endStation.NameGermanNew, endStation.Location);

            await this.target.SegmentAggregates.AddAsync(segmentAggregate, cancellationToken);
        }

        await this.target.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}