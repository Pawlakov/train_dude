// <copyright file="StationAggregateProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Stations;

using System;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Stations.Domain.Events;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class StationAggregateProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public StationAggregateProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task StationCreated_SetsInitialAggregateState()
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(stationId, new StationCreated(stationId, Who, "Lublinitz", "Loben", "Lubliniec", null));
        await session.SaveChangesAsync();

        var aggregate = await session.LoadAsync<StationAggregate>(stationId);
        await Assert.That(aggregate).IsNotNull();
        await Assert.That(aggregate.Id).IsEqualTo(stationId);
        await Assert.That(aggregate.AxleCount).IsEqualTo(1);
        await Assert.That(aggregate.Location).IsNull();
        await Assert.That(aggregate.NameGerman).IsEqualTo("Lublinitz");
        await Assert.That(aggregate.NameGermanNew).IsEqualTo("Loben");
        await Assert.That(aggregate.NamePolish).IsEqualTo("Lubliniec");
        await Assert.That(aggregate.NameRussian).IsNull();
    }

    [Test]
    public async Task StationLocationSet_UpdatesLocationWithoutAffectingOtherFields()
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();
        var location = new Location(20, 50);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Lublinitz", "Loben", "Lubliniec", null));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationLocationSet(stationId, Who, location));
            await session.SaveChangesAsync();

            var aggregate = await session.LoadAsync<StationAggregate>(stationId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.Location).IsEqualTo(location);
            await Assert.That(aggregate.AxleCount).IsEqualTo(1);
            await Assert.That(aggregate.NameGermanNew).IsEqualTo("Loben");
        }
    }

    [Test]
    public async Task StationLocationSet_AppliedTwice_KeepsMostRecentLocation()
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();
        var firstLocation = new Location(20, 50);
        var secondLocation = new Location(21, 51);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Lublinitz", "Loben", "Lubliniec", null));
            session.Events.Append(stationId, new StationLocationSet(stationId, Who, firstLocation));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationLocationSet(stationId, Who, secondLocation));
            await session.SaveChangesAsync();

            var aggregate = await session.LoadAsync<StationAggregate>(stationId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.Location).IsEqualTo(secondLocation);
        }
    }

    [Test]
    [Arguments(1)]
    [Arguments(3)]
    public async Task StationAxleAdded_IncrementsAxleCountForEachEvent(int axlesAdded)
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Lublinitz", "Loben", "Lubliniec", null));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            for (var i = 0; i < axlesAdded; i++)
            {
                session.Events.Append(stationId, new StationAxleAdded(stationId, Who));
            }

            await session.SaveChangesAsync();

            var aggregate = await session.LoadAsync<StationAggregate>(stationId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.AxleCount).IsEqualTo(1 + axlesAdded);
        }
    }

    [Test]
    public async Task FullEventHistory_AppliesEventsInOrderAcrossMultipleSessions()
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();
        var location = new Location(20, 50);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Gerdauen", null, null, "Железнодорожный"));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationLocationSet(stationId, Who, location));
            session.Events.Append(stationId, new StationAxleAdded(stationId, Who));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationAxleAdded(stationId, Who));
            await session.SaveChangesAsync();

            var aggregate = await session.LoadAsync<StationAggregate>(stationId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.AxleCount).IsEqualTo(3);
            await Assert.That(aggregate.Location).IsEqualTo(location);
            await Assert.That(aggregate.NameGerman).IsEqualTo("Gerdauen");
            await Assert.That(aggregate.NameRussian).IsEqualTo("Железнодорожный");
        }
    }

    [Test]
    public async Task SettingsNamingPolicySet_DoesNotAffectStationAggregate()
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, Who));
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Lublinitz", "Loben", "Lubliniec", null));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, Who, NamingPolicy.German));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            var aggregate = await session.LoadAsync<StationAggregate>(stationId);
            await Assert.That(aggregate).IsNotNull();
            await Assert.That(aggregate.NameGerman).IsEqualTo("Lublinitz");
            await Assert.That(aggregate.NameGermanNew).IsEqualTo("Loben");
            await Assert.That(aggregate.NamePolish).IsEqualTo("Lubliniec");
            await Assert.That(aggregate.AxleCount).IsEqualTo(1);
        }
    }
}