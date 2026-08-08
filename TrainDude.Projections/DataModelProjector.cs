// <copyright file="DataModelProjector.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Projections;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using Marten;

using TrainDude.Commands.Data;
using TrainDude.Queries.Data;
using TrainDude.Queries.Data.Documents;

public sealed class DataModelProjector
{
    private readonly IDocumentSession source;
    private readonly ILiteCollection<Line> linesTarget;
    private readonly ILiteCollection<Radius> radiiTarget;
    private readonly ILiteCollection<Segment> segmentsTarget;
    private readonly ILiteCollection<Station> stationsTarget;
    private readonly ILiteCollection<Trip> tripsTarget;

    public DataModelProjector(IDocumentSession source, ILiteCollection<Line> linesTarget, ILiteCollection<Radius> radiiTarget, ILiteCollection<Segment> segmentsTarget, ILiteCollection<Station> stationsTarget, ILiteCollection<Trip> tripsTarget)
    {
        this.source = source;
        this.linesTarget = linesTarget;
        this.radiiTarget = radiiTarget;
        this.segmentsTarget = segmentsTarget;
        this.stationsTarget = stationsTarget;
        this.tripsTarget = tripsTarget;
    }

    public async Task RebuildAsync(CancellationToken cancellationToken = default)
    {
        var stations = await this.source
            .Query<Commands.Data.Documents.Station>()
            .ToListAsync(cancellationToken);

        var trips = await this.source
            .Query<Commands.Data.Documents.Trip>()
            .ToListAsync(cancellationToken);

        var lines = await this.source
            .Query<Commands.Data.Documents.Line>()
            .ToListAsync(cancellationToken);

        /*var radii = await this.source.Radii
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var segments = await this.source.Segments
            .AsNoTracking()
            .Include(s => s.Extremes)
            .ThenInclude(e => e.Station)
            .ToListAsync(cancellationToken);*/

        this.tripsTarget.DeleteAll();
        this.segmentsTarget.DeleteAll();
        this.stationsTarget.DeleteAll();
        this.linesTarget.DeleteAll();
        this.radiiTarget.DeleteAll();

        foreach (var station in stations)
        {
            var stationAggregate = new Station
            {
                StationId = station.Id,
                NameGerman = station.NameGerman,
                /*NameGermanNew = station.NameGermanNew,*/
                Location = station.Location,
            };

            this.stationsTarget.Insert(station.Id, stationAggregate);
        }

        foreach (var trip in trips)
        {
            var tripAggregate = new Trip
            {
                TripId = trip.Id,
                TripNumber = trip.TripNumber,
            };

            this.tripsTarget.Insert(trip.Id, tripAggregate);
        }

        foreach (var line in lines)
        {
            var lineTrips = new List<Line.LineTrip>();
            foreach (var tripId in line.Trips)
            {
                var trip = await this.source.LoadAsync<Commands.Data.Documents.Trip>(tripId, cancellationToken);

                lineTrips.Add(new Line.LineTrip { TripId = trip.Id, TripNumber = trip.TripNumber });
            }

            var lineStations = new List<Line.LineStation>();
            foreach (var stationId in line.Stations)
            {
                var station = await this.source.LoadAsync<Commands.Data.Documents.Station>(stationId, cancellationToken);

                lineStations.Add(new Line.LineStation { StationId = station.Id, NameGerman = station.NameGerman });
            }

            var lineAggregate = new Line
            {
                LineId = line.Id,
                LineNumber = line.LineNumber,
                LineLetter = line.LineLetter,
                LineDesignation = $"{line.LineNumber}{line.LineLetter}",
                Trips = lineTrips.ToImmutableList(),
                Stations = lineStations.ToImmutableList(),
            };

            this.linesTarget.Insert(line.Id, lineAggregate);
        }

        /*foreach (var radius in radii)
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
                Vertices = segment.Vertices.OrderBy(x => x.OrdinalId).Select(x => x.Location).ToList(),
            };

            this.segmentsTarget.Insert(segment.SegmentId, segmentAggregate);
        }*/
    }
}