// <copyright file="SegmentReadModelProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Infrastructure.Tests.Segments;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.ReadModels;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class SegmentReadModelProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public SegmentReadModelProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task SegmentCreated_WithNoSettingsRecorded_DefaultsToModernNamingPolicy()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var a = new SegmentEnd(station1Id, 0, true);
        var b = new SegmentEnd(station2Id, 0, true);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(station1Id, new StationCreated(station1Id, Who, "Lublinitz", "Loben", "Lubliniec", null));
            session.Events.Append(station2Id, new StationCreated(station2Id, Who, "Gerdauen", null, null, "Железнодорожный"));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, a, b));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentReadModel>(CancellationToken.None);

            var segment = await session.LoadAsync<SegmentReadModel>(segmentId);
            await Assert.That(segment).IsNotNull();
            await Assert.That(segment.NominalLength).IsEqualTo(22.2);
            await Assert.That(segment.Haversine).IsNull();
            await Assert.That(segment.Tracks).IsEqualTo(2);
            await Assert.That(segment.A.Id).IsEqualTo(station1Id);
            await Assert.That(segment.A.Axle).IsEqualTo(0);
            await Assert.That(segment.A.Pole).IsTrue();
            await Assert.That(segment.A.Location).IsNull();
            await Assert.That(segment.A.Name).IsEqualTo("Lubliniec");
            await Assert.That(segment.B.Id).IsEqualTo(station2Id);
            await Assert.That(segment.B.Axle).IsEqualTo(0);
            await Assert.That(segment.B.Pole).IsTrue();
            await Assert.That(segment.B.Location).IsNull();
            await Assert.That(segment.B.Name).IsEqualTo("Железнодорожный");
            await Assert.That(segment.Course).IsEmpty();
        }
    }

    [Test]
    public async Task SegmentCreated_WithSettingsRecorded_UsesConfiguredNamingPolicy()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var a = new SegmentEnd(station1Id, 0, true);
        var b = new SegmentEnd(station2Id, 0, true);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, Who));
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, Who, NamingPolicy.German));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(station1Id, new StationCreated(station1Id, Who, "Lublinitz", "Loben", "Lubliniec", null));
            session.Events.Append(station2Id, new StationCreated(station2Id, Who, "Gerdauen", null, null, "Железнодорожный"));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, a, b));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentReadModel>(CancellationToken.None);

            var segment = await session.LoadAsync<SegmentReadModel>(segmentId);
            await Assert.That(segment).IsNotNull();
            await Assert.That(segment.A.Name).IsEqualTo("Loben");
            await Assert.That(segment.B.Name).IsEqualTo("Gerdauen");
        }
    }

    [Test]
    public async Task StationLocationSet_UpdatesLocation_AndRecalculatesHaversine()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var loc1 = new Location(50.66, 18.68);
        var loc2 = new Location(54.36, 21.30);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(station1Id, new StationCreated(station1Id, Who, "Lublinitz", "Loben", "Lubliniec", null));
            session.Events.Append(station2Id, new StationCreated(station2Id, Who, "Gerdauen", null, null, "Железнодорожный"));
            session.Events.Append(station2Id, new StationLocationSet(station2Id, Who, loc2));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, new SegmentEnd(station1Id, 0, true), new SegmentEnd(station2Id, 0, true)));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new StationLocationSet(station1Id, Who, loc1));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentReadModel>(CancellationToken.None);

            var segment = await session.LoadAsync<SegmentReadModel>(segmentId);

            await Assert.That(segment).IsNotNull();
            await Assert.That(segment.A.Location).IsNotNull();
            await Assert.That(segment.A.Location).IsEqualTo(loc1);
            await Assert.That(segment.B.Location).IsNotNull();
            await Assert.That(segment.B.Location).IsEqualTo(loc2);
            await Assert.That(segment.Haversine).IsNotNull();
            await Assert.That(segment.Haversine.Value).IsGreaterThan(0.0);
        }
    }

    [Test]
    public async Task SegmentCourseSet_WhenStationsHaveLocations_CalculatesHaversineWithCourse()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var loc1 = new Location(50.66, 18.68);
        var loc2 = new Location(54.36, 21.30);
        var course = new[] { new Location(52.0, 19.0), new Location(53.0, 20.0) };

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(station1Id, new StationCreated(station1Id, Who, "Lublinitz", "Loben", "Lubliniec", null));
            session.Events.Append(station1Id, new StationLocationSet(station1Id, Who, loc1));
            session.Events.Append(station2Id, new StationCreated(station2Id, Who, "Gerdauen", null, null, "Железнодорожный"));
            session.Events.Append(station2Id, new StationLocationSet(station2Id, Who, loc2));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, new SegmentEnd(station1Id, 0, true), new SegmentEnd(station2Id, 0, true)));
            session.Events.Append(segmentId, new SegmentCourseSet(segmentId, Who, course));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentReadModel>(CancellationToken.None);

            var segment = await session.LoadAsync<SegmentReadModel>(segmentId);

            await Assert.That(segment).IsNotNull();
            await Assert.That(segment.Course).IsNotEmpty();
            await Assert.That(segment.Course.Count).IsEqualTo(2);
            await Assert.That(segment.Haversine).IsNotNull();
            await Assert.That(segment.Haversine.Value).IsGreaterThan(0.0);
        }
    }

    [Test]
    public async Task SettingsNamingPolicySet_UpdatesStationNamesAccordingToNewPolicy()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(station1Id, new StationCreated(station1Id, Who, "Lublinitz", "Loben", "Lubliniec", null));
            session.Events.Append(station2Id, new StationCreated(station2Id, Who, "Gerdauen", null, null, "Железнодорожный"));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, new SegmentEnd(station1Id, 0, true), new SegmentEnd(station2Id, 0, true)));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SettingsNamingPolicySet(SettingsSingleton.Id, Who, NamingPolicy.German));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentReadModel>(CancellationToken.None);

            var segment = await session.LoadAsync<SegmentReadModel>(segmentId);

            await Assert.That(segment).IsNotNull();
            await Assert.That(segment.A.Name).IsEqualTo("Loben");
            await Assert.That(segment.B.Name).IsEqualTo("Gerdauen");
        }
    }
}