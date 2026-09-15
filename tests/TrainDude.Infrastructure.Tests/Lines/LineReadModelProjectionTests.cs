// <copyright file="LineReadModelProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Lines;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Lines.ReadModels.Values;
using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Settings;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Trips.Domain.Events;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerTestSession)]
public class LineReadModelProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public LineReadModelProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Before(Test)]
    public async Task SetUp()
    {
        await this.fixture.ResetAsync();
    }

    [Test]
    [Arguments(120, null, "120")]
    [Arguments(110, 'f', "110f")]
    public async Task LineCreated(int lineNumber, char? lineLetter, string lineDesignation)
    {
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
        var lineId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(tripId, new TripCreated(tripId, Who, 123));
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
        var lineId = Guid.NewGuid();
        var trip1Id = Guid.NewGuid();
        var trip2Id = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(trip1Id, new TripCreated(trip1Id, Who, 101));
            session.Events.Append(trip2Id, new TripCreated(trip2Id, Who, 202));
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
    public async Task LineSegmentAppended_ResolvesReferences()
    {
        var lineId = Guid.NewGuid();
        var segment1Id = Guid.NewGuid();
        var segment2Id = Guid.NewGuid();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var station3Id = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(station1Id, new StationCreated(station1Id, Who, "A-old", "A-new", "A", null));
            session.Events.Append(station2Id, new StationCreated(station2Id, Who, "B-old", "B-new", null, "B"));
            var end1 = new SegmentEnd(station1Id, 0, true);
            var end2 = new SegmentEnd(station2Id, 0, false);
            session.Events.Append(segment1Id, new SegmentCreated(segment1Id, Who, 10, 1, end1, end2));
            session.Events.Append(lineId, new LineCreated(lineId, Who, 120, null));
            session.Events.Append(lineId, new LineSegmentAppended(lineId, Who, segment1Id));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<LineReadModel>(CancellationToken.None);

            var line = await session.LoadAsync<LineReadModel>(lineId);
            await Assert.That(line).IsNotNull();
            await Assert.That(line.Segments.Count).IsEqualTo(1);
            await Assert.That(line.Segments[0].A.Id).IsEqualTo(station1Id);
            await Assert.That(line.Segments[0].B.Id).IsEqualTo(station2Id);
            await Assert.That(line.Segments[0].A.Name).IsEqualTo("A");
            await Assert.That(line.Segments[0].B.Name).IsEqualTo("B");
            await Assert.That(line.Stations.Count).IsEqualTo(2);
            await Assert.That(line.Stations[0].Id).IsEqualTo(station1Id);
            await Assert.That(line.Stations[1].Id).IsEqualTo(station2Id);
            await Assert.That(line.Stations[0].Name).IsEqualTo("A");
            await Assert.That(line.Stations[1].Name).IsEqualTo("B");
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsAccessor.SingletonId, new SettingsCreated(SettingsAccessor.SingletonId, Who));
            session.Events.Append(SettingsAccessor.SingletonId, new SettingsNamingPolicySet(SettingsAccessor.SingletonId, Who, NamingPolicy.German));
            session.Events.Append(station3Id, new StationCreated(station3Id, Who, "C-old", null, "C", null));
            var end3 = new SegmentEnd(station2Id, 0, true);
            var end4 = new SegmentEnd(station3Id, 0, false);
            session.Events.Append(segment2Id, new SegmentCreated(segment2Id, Who, 20, 1, end3, end4));
            session.Events.Append(lineId, new LineSegmentAppended(lineId, Who, segment2Id));
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
            await Assert.That(line.Segments[1].A.Id).IsEqualTo(station2Id);
            await Assert.That(line.Segments[1].B.Id).IsEqualTo(station3Id);
            await Assert.That(line.Segments[0].A.Name).IsEqualTo("A-new");
            await Assert.That(line.Segments[0].B.Name).IsEqualTo("B-new");
            await Assert.That(line.Segments[1].A.Name).IsEqualTo("B-new");
            await Assert.That(line.Segments[1].B.Name).IsEqualTo("C-old");
            await Assert.That(line.Stations.Count).IsEqualTo(3);
            await Assert.That(line.Stations[0].Id).IsEqualTo(station1Id);
            await Assert.That(line.Stations[1].Id).IsEqualTo(station2Id);
            await Assert.That(line.Stations[2].Id).IsEqualTo(station3Id);
            await Assert.That(line.Stations[0].Name).IsEqualTo("A-new");
            await Assert.That(line.Stations[1].Name).IsEqualTo("B-new");
            await Assert.That(line.Stations[2].Name).IsEqualTo("C-old");
        }
    }
}