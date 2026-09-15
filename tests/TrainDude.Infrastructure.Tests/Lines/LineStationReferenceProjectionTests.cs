// <copyright file="LineStationReferenceProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Lines;

using System;
using System.Threading.Tasks;

using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Stations.Domain.Events;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerTestSession)]
public class LineStationReferenceProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public LineStationReferenceProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Before(Test)]
    public async Task SetUp()
    {
        await this.fixture.ResetAsync();
    }

    [Test]
    public async Task StationCreated()
    {
        var stationId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Darkehmen", "Angerapp", null, "Озёрск"));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            var station = await session.LoadAsync<LineStationReference>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.AxleCount).IsEqualTo(1);
            await Assert.That(station.Location).IsNull();
            await Assert.That(station.NameGerman).IsEqualTo("Darkehmen");
            await Assert.That(station.NameGermanNew).IsEqualTo("Angerapp");
            await Assert.That(station.NamePolish).IsNull();
            await Assert.That(station.NameRussian).IsEqualTo("Озёрск");
        }
    }

    [Test]
    public async Task StationLocationSet_UpdatesLocation()
    {
        var stationId = Guid.NewGuid();
        var location = new TrainDude.Features.Shared.Contracts.Values.Location(50.66, 18.68);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Darkehmen", "Angerapp", null, "Озёрск"));
            session.Events.Append(stationId, new StationLocationSet(stationId, Who, location));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            var station = await session.LoadAsync<LineStationReference>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.Location).IsEqualTo(location);
            await Assert.That(station.AxleCount).IsEqualTo(1);
        }
    }

    [Test]
    public async Task StationAxleAdded_IncrementsAxleCount()
    {
        var stationId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Darkehmen", "Angerapp", null, "Озёрск"));
            session.Events.Append(stationId, new StationAxleAdded(stationId, Who));
            session.Events.Append(stationId, new StationAxleAdded(stationId, Who));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            var station = await session.LoadAsync<LineStationReference>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.AxleCount).IsEqualTo(3);
        }
    }
}