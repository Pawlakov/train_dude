// <copyright file="LineReadModelProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Lines;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.Domain.Values;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Trips.Domain.Events;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class LineReadModelProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public LineReadModelProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    [Arguments(120, null, "120")]
    [Arguments(110, 'f', "110f")]
    public async Task LineCreated(int lineNumber, char? lineLetter, string lineDesignation)
    {
        await this.fixture.ResetAsync();
        var lineId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(lineId, new LineCreated(lineId, Who, lineNumber, lineLetter));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<LineReadModel>(CancellationToken.None);

            var line = await session.LoadAsync<LineReadModel>(lineId);
            await Assert.That(line).IsNotNull();
            await Assert.That(line.LineNumber).IsEqualTo(lineNumber);
            await Assert.That(line.LineLetter).IsEqualTo(lineLetter);
            await Assert.That(line.LineDesignation).IsEqualTo(lineDesignation);
            await Assert.That(line.Trips).IsEmpty();
            await Assert.That(line.Segments).IsEmpty();
            await Assert.That(line.Stations).IsEmpty();
        }
    }

    [Test]
    public async Task LineTripAssigned_ResolvesTripReferenceAndAddsTripToLine()
    {
        await this.fixture.ResetAsync();
        var lineId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(tripId, CreateTripCreated(tripId, 123));
            session.Events.Append(lineId, new LineCreated(lineId, Who, 120, null));
            session.Events.Append(lineId, new LineTripAssigned(lineId, Who, tripId));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<LineReadModel>(CancellationToken.None);

            var line = await session.LoadAsync<LineReadModel>(lineId);
            await Assert.That(line).IsNotNull();
            await Assert.That(line.Trips.Count).IsEqualTo(1);
            await Assert.That(line.Trips[0].Id).IsEqualTo(tripId);
            await Assert.That(line.Trips[0].Number).IsEqualTo(123);
        }
    }

    [Test]
    public async Task MultipleLineTripAssignedEvents_PreserveEventOrder()
    {
        await this.fixture.ResetAsync();
        var lineId = Guid.NewGuid();
        var trip1Id = Guid.NewGuid();
        var trip2Id = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(trip1Id, CreateTripCreated(trip1Id, 101));
            session.Events.Append(trip2Id, CreateTripCreated(trip2Id, 202));
            session.Events.Append(lineId, new LineCreated(lineId, Who, 120, null));
            session.Events.Append(lineId, new LineTripAssigned(lineId, Who, trip1Id));
            session.Events.Append(lineId, new LineTripAssigned(lineId, Who, trip2Id));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<LineReadModel>(CancellationToken.None);

            var line = await session.LoadAsync<LineReadModel>(lineId);
            await Assert.That(line).IsNotNull();
            await Assert.That(line.Trips.Count).IsEqualTo(2);
            await Assert.That(line.Trips[0].Id).IsEqualTo(trip1Id);
            await Assert.That(line.Trips[0].Number).IsEqualTo(101);
            await Assert.That(line.Trips[1].Id).IsEqualTo(trip2Id);
            await Assert.That(line.Trips[1].Number).IsEqualTo(202);
        }
    }

    [Test]
    public async Task LineSegmentAppended_ResolvesSegmentReferenceAndBuildsStationPath()
    {
        await this.fixture.ResetAsync();
        var lineId = Guid.NewGuid();
        var segment1Id = Guid.NewGuid();
        var segment2Id = Guid.NewGuid();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var station3Id = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(station1Id, new StationCreated(station1Id, Who, "A-old", "A-new", "A", null));
            session.Events.Append(station2Id, new StationCreated(station2Id, Who, "B-old", "B-new", "B", null));
            session.Events.Append(station3Id, new StationCreated(station3Id, Who, "C-old", "C-new", "C", null));
            session.Events.Append(
            segment1Id,
            new SegmentCreated(
            segment1Id,
            Who,
            10,
            1,
            new SegmentEnd(station1Id, 0, true),
            new SegmentEnd(station2Id, 0, true)));
            session.Events.Append(
            segment2Id,
            new SegmentCreated(
            segment2Id,
            Who,
            20,
            1,
            new SegmentEnd(station3Id, 0, true),
            new SegmentEnd(station2Id, 0, true)));
            session.Events.Append(lineId, new LineCreated(lineId, Who, 120, null));
            session.Events.Append(lineId, CreateLineSegmentAppended(lineId, segment1Id, station1Id, station2Id));
            session.Events.Append(lineId, CreateLineSegmentAppended(lineId, segment2Id, station2Id, station3Id));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<LineReadModel>(CancellationToken.None);

            var line = await session.LoadAsync<LineReadModel>(lineId);
            await Assert.That(line).IsNotNull();
            await Assert.That(line.Segments.Count).IsEqualTo(2);
            await Assert.That(line.Segments[0].A.Id).IsEqualTo(station1Id);
            await Assert.That(line.Segments[0].B.Id).IsEqualTo(station2Id);
            await Assert.That(line.Segments[1].A.Id).IsEqualTo(station3Id);
            await Assert.That(line.Segments[1].B.Id).IsEqualTo(station2Id);
            await Assert.That(line.Stations.Count).IsEqualTo(3);
            await Assert.That(line.Stations[0].Id).IsEqualTo(station1Id);
            await Assert.That(line.Stations[1].Id).IsEqualTo(station2Id);
            await Assert.That(line.Stations[2].Id).IsEqualTo(station3Id);
        }
    }

    private static TripCreated CreateTripCreated(Guid tripId, int tripNumber)
    {
        return (TripCreated)CreateRecord(
        typeof(TripCreated),
        new Dictionary<string, object?>
        {
            ["id"] = tripId,
            ["tripId"] = tripId,
            ["who"] = Who,
            ["tripNumber"] = tripNumber,
        });
    }

    private static LineSegment CreateLineSegment(Guid segmentId, SegmentEnd a, SegmentEnd b)
    {
        return (LineSegment)CreateRecord(
        typeof(LineSegment),
        new Dictionary<string, object?>
        {
            ["id"] = segmentId,
            ["segmentId"] = segmentId,
            ["a"] = a,
            ["b"] = b,
        });
    }

    private static LineSegmentAppended CreateLineSegmentAppended(Guid lineId, Guid segmentId, Guid aId, Guid bId)
    {
        var a = new SegmentEnd(aId, 0, true);
        var b = new SegmentEnd(bId, 0, true);

        return (LineSegmentAppended)CreateRecord(
        typeof(LineSegmentAppended),
        new Dictionary<string, object?>
        {
            ["id"] = lineId,
            ["lineId"] = lineId,
            ["segmentId"] = segmentId,
            ["a"] = a,
            ["b"] = b,
            ["segment"] = CreateLineSegment(segmentId, a, b),
            ["lineSegment"] = CreateLineSegment(segmentId, a, b),
            ["who"] = Who,
            ["when"] = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        });
    }

    private static object CreateRecord(Type type, IReadOnlyDictionary<string, object?> values)
    {
        foreach (var constructor in type.GetConstructors())
        {
            var parameters = constructor.GetParameters();
            var arguments = new object?[parameters.Length];
            var supported = true;

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                if (TryGetValue(values, parameter.Name!, out var value))
                {
                    arguments[i] = value;
                    continue;
                }

                if (parameter.HasDefaultValue)
                {
                    arguments[i] = parameter.DefaultValue;
                    continue;
                }

                if (parameter.ParameterType == typeof(Guid))
                {
                    arguments[i] = Guid.NewGuid();
                }
                else if (parameter.ParameterType == typeof(string))
                {
                    arguments[i] = Who;
                }
                else if (parameter.ParameterType == typeof(DateTime))
                {
                    arguments[i] = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                }
                else if (parameter.ParameterType == typeof(int))
                {
                    arguments[i] = 0;
                }
                else if (parameter.ParameterType == typeof(long))
                {
                    arguments[i] = 0L;
                }
                else if (parameter.ParameterType == typeof(bool))
                {
                    arguments[i] = false;
                }
                else if (parameter.ParameterType.IsEnum)
                {
                    arguments[i] = Activator.CreateInstance(parameter.ParameterType);
                }
                else
                {
                    supported = false;
                    break;
                }
            }

            if (supported)
            {
                return constructor.Invoke(arguments);
            }
        }

        throw new InvalidOperationException($"Could not construct {type.FullName} using the supplied values.");
    }

    private static bool TryGetValue(IReadOnlyDictionary<string, object?> values, string name, out object? value)
    {
        foreach (var pair in values)
        {
            if (string.Equals(pair.Key, name, StringComparison.OrdinalIgnoreCase))
            {
                value = pair.Value;
                return true;
            }
        }

        value = null;
        return false;
    }
}