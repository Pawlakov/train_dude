// <copyright file="DataModelProjector.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Projections;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Microsoft.EntityFrameworkCore;

using TrainDude.Commands.Data;
using TrainDude.Queries.Data;
using TrainDude.Queries.Data.Aggregates;

public sealed class DataModelProjector
{
    private readonly IWriteDbContext source;
    private readonly ILiteCollection<Radius> radiiTarget;
    private readonly ILiteCollection<Segment> segmentsTarget;
    private readonly ILiteCollection<Station> stationsTarget;

    public DataModelProjector(IWriteDbContext source, ILiteCollection<Radius> radiiTarget, ILiteCollection<Segment> segmentsTarget, ILiteCollection<Station> stationsTarget)
    {
        this.source = source;
        this.radiiTarget = radiiTarget;
        this.segmentsTarget = segmentsTarget;
        this.stationsTarget = stationsTarget;
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

        this.segmentsTarget.DeleteAll();
        this.stationsTarget.DeleteAll();
        this.radiiTarget.DeleteAll();

        foreach (var station in stations)
        {
            var stationAggregate = new Station
            {
                StationId = station.StationId,
                NameGerman = station.NameGerman,
                NameGermanNew = station.NameGermanNew,
                Location = station.Location,
            };

            this.stationsTarget.Insert(station.StationId, stationAggregate);
        }

        foreach (var radius in radii)
        {
            var radiusAggregate = new Radius
            {
                RadiusId = radius.RadiusId,
                Speed = radius.Speed,
                Minimum = radius.Minimum,
            };

            this.radiiTarget.Insert(radius.RadiusId, radiusAggregate);
        }

        foreach (var segment in segments)
        {
            var startStation = segment.Extremes.Single(e => !e.IsEnd).Station;
            var endStation = segment.Extremes.Single(e => e.IsEnd).Station;

            var segmentAggregate = new Segment
            {
                SegmentId = segment.SegmentId,
                NominalLength = segment.NominalLength,
                AStationId = startStation.StationId,
                AName = startStation.NameGermanNew ?? startStation.NameGerman,
                ALocation = startStation.Location,
                BStationId = endStation.StationId,
                BName = endStation.NameGermanNew ?? endStation.NameGerman,
                BLocation = endStation.Location,
            };

            this.segmentsTarget.Insert(segment.SegmentId, segmentAggregate);
        }
    }
}