// <copyright file="LineSegmentReferenceProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Lines;

using System;
using System.Threading.Tasks;

using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Segments.Domain.Events;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerTestSession)]
public class LineSegmentReferenceProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public LineSegmentReferenceProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Before(Test)]
    public async Task SetUp()
    {
        await this.fixture.ResetAsync();
    }

    [Test]
    public async Task SegmentCreated_CreatesLineSegmentReference()
    {
        var segmentId = Guid.NewGuid();
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(
            segmentId,
            new SegmentCreated(
            segmentId,
            Who,
            22.2,
            2,
            new TrainDude.Features.Segments.Domain.Values.SegmentEnd(station1Id, 0, true),
            new TrainDude.Features.Segments.Domain.Values.SegmentEnd(station2Id, 0, true)));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            var reference = await session.LoadAsync<LineSegmentReference>(segmentId);

            await Assert.That(reference).IsNotNull();
            await Assert.That(reference.Id).IsEqualTo(segmentId);
        }
    }
}