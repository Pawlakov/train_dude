// <copyright file="LineTripReferenceProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Lines;

using System;
using System.Threading.Tasks;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Trips.Domain.Events;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerTestSession)]
public class LineTripReferenceProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public LineTripReferenceProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task TripCreated_CreatesLineTripReference()
    {
        await this.fixture.ResetAsync();
        var tripId = Guid.NewGuid();

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(tripId, new TripCreated(tripId, Who, 123));
        await session.SaveChangesAsync();

        var reference = await session.LoadAsync<LineTripReference>(tripId);

        await Assert.That(reference).IsNotNull();
        await Assert.That(reference.Id).IsEqualTo(tripId);
        await Assert.That(reference.Number).IsEqualTo(123);
    }
}