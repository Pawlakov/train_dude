// <copyright file="SegmentReadModelProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Infrastructure.Tests;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Segments.ReadModels;
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

            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

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
}