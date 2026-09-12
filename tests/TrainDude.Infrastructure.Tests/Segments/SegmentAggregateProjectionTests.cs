// <copyright file="SegmentAggregateProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Segments;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Shared.Contracts.Values;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class SegmentAggregateProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public SegmentAggregateProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task SegmentCreated_SetsInitialAggregateState()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();

        var a = new SegmentEnd(station1Id, 3, true);
        var b = new SegmentEnd(station2Id, 5, false);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, a, b));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentAggregate>(CancellationToken.None);

            var aggregate = await session.LoadAsync<SegmentAggregate>(segmentId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.Id).IsEqualTo(segmentId);
            await Assert.That(aggregate.NominalLength).IsEqualTo(22.2);
            await Assert.That(aggregate.Tracks).IsEqualTo(2);
            await Assert.That(aggregate.A.Id).IsEqualTo(station1Id);
            await Assert.That(aggregate.A.Axle).IsEqualTo(3);
            await Assert.That(aggregate.A.Pole).IsTrue();
            await Assert.That(aggregate.B.Id).IsEqualTo(station2Id);
            await Assert.That(aggregate.B.Axle).IsEqualTo(5);
            await Assert.That(aggregate.B.Pole).IsFalse();
            await Assert.That(aggregate.Course).IsEmpty();
        }
    }

    [Test]
    public async Task SegmentCourseSet_SetsCourseWithoutAffectingOtherFields()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var course = new[] { new Location(52.0, 19.0), new Location(53.0, 20.0) };

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, new SegmentEnd(station1Id, 0, true), new SegmentEnd(station2Id, 0, true)));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCourseSet(segmentId, Who, course));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentAggregate>(CancellationToken.None);

            var aggregate = await session.LoadAsync<SegmentAggregate>(segmentId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.Course.Count).IsEqualTo(2);
            await Assert.That(aggregate.Course[0]).IsEqualTo(course[0]);
            await Assert.That(aggregate.Course[1]).IsEqualTo(course[1]);
            await Assert.That(aggregate.NominalLength).IsEqualTo(22.2);
            await Assert.That(aggregate.Tracks).IsEqualTo(2);
            await Assert.That(aggregate.A.Id).IsEqualTo(station1Id);
            await Assert.That(aggregate.B.Id).IsEqualTo(station2Id);
        }
    }

    [Test]
    public async Task SegmentCourseSet_AppliedTwice_KeepsMostRecentCourse()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var firstCourse = new[] { new Location(52.0, 19.0) };
        var secondCourse = new[] { new Location(53.0, 20.0), new Location(53.5, 20.5) };

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, new SegmentEnd(station1Id, 0, true), new SegmentEnd(station2Id, 0, true)));
            session.Events.Append(segmentId, new SegmentCourseSet(segmentId, Who, firstCourse));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCourseSet(segmentId, Who, secondCourse));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentAggregate>(CancellationToken.None);

            var aggregate = await session.LoadAsync<SegmentAggregate>(segmentId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.Course.Count).IsEqualTo(2);
            await Assert.That(aggregate.Course[0]).IsEqualTo(secondCourse[0]);
            await Assert.That(aggregate.Course[1]).IsEqualTo(secondCourse[1]);
        }
    }

    [Test]
    public async Task SegmentCourseSet_WithEmptyCourse_ClearsPreviouslySetCourse()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var course = new[] { new Location(52.0, 19.0), new Location(53.0, 20.0) };

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 22.2, 2, new SegmentEnd(station1Id, 0, true), new SegmentEnd(station2Id, 0, true)));
            session.Events.Append(segmentId, new SegmentCourseSet(segmentId, Who, course));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCourseSet(segmentId, Who, Array.Empty<Location>()));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentAggregate>(CancellationToken.None);

            var aggregate = await session.LoadAsync<SegmentAggregate>(segmentId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.Course).IsEmpty();
        }
    }

    [Test]
    public async Task FullEventHistory_AppliesEventsInOrderAcrossMultipleSessions()
    {
        await this.fixture.ResetAsync();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var segmentId = Guid.NewGuid();
        var course = new[] { new Location(52.0, 19.0), new Location(53.0, 20.0) };

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCreated(segmentId, Who, 15.0, 1, new SegmentEnd(station1Id, 0, true), new SegmentEnd(station2Id, 0, false)));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(segmentId, new SegmentCourseSet(segmentId, Who, course));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<SegmentAggregate>(CancellationToken.None);

            var aggregate = await session.LoadAsync<SegmentAggregate>(segmentId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.NominalLength).IsEqualTo(15.0);
            await Assert.That(aggregate.Tracks).IsEqualTo(1);
            await Assert.That(aggregate.A.Pole).IsTrue();
            await Assert.That(aggregate.B.Pole).IsFalse();
            await Assert.That(aggregate.Course.Count).IsEqualTo(2);
        }
    }
}