// <copyright file="StationReadModelProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.ReadModels;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class StationReadModelProjectionTests
{
    private readonly ProjectionStoreFixture fixture;

    public StationReadModelProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task StationCreated_UsesNamingPolicyInEffectAtCreationTime()
    {
        await this.fixture.ResetAsync();
        var who = "test@example.com";
        var stationId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, who));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.German));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<SharedSettingsReference>(CancellationToken.None);

            session.Events.Append(stationId, new StationCreated(stationId, who, "Lublinitz", "Loben", "Lubliniec", null));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var station = await session.LoadAsync<StationReadModel>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.AxleCount).IsEqualTo(1);
            await Assert.That(station.Location).IsNull();
            await Assert.That(station.NameGerman).IsEqualTo("Lublinitz");
            await Assert.That(station.NameGermanNew).IsEqualTo("Loben");
            await Assert.That(station.NamePolish).IsEqualTo("Lubliniec");
            await Assert.That(station.Name).IsEqualTo("Loben");
        }
    }

    [Test]
    public async Task NamingPolicyRevertedToModern_UpdatesStationNameAndLocation()
    {
        await this.fixture.ResetAsync();
        var who = "test@example.com";
        var stationId = Guid.NewGuid();
        var location = new Location(20, 50);

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, who));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.German));
            session.Events.Append(stationId, new StationCreated(stationId, who, "Lublinitz", "Loben", "Lubliniec", null));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationLocationSet(stationId, who, location));
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.Modern));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var station = await session.LoadAsync<StationReadModel>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.AxleCount).IsEqualTo(1);
            await Assert.That(station.Location).IsEqualTo(location);
            await Assert.That(station.NameGerman).IsEqualTo("Lublinitz");
            await Assert.That(station.NameGermanNew).IsEqualTo("Loben");
            await Assert.That(station.NamePolish).IsEqualTo("Lubliniec");
            await Assert.That(station.Name).IsEqualTo("Lubliniec");
        }
    }
}