// <copyright file="StationReadModelProjectionTests.cs" company="Pawlakov">
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
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.ReadModels;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class StationReadModelProjectionTests
{
    private const string Who = "test@example.com";

    private readonly ProjectionStoreFixture fixture;

    public StationReadModelProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task StationCreated_WithNoSettingsRecorded_DefaultsToModernNamingPolicy()
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Darkehmen", "Angerapp", null, "Озёрск"));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var station = await session.LoadAsync<StationReadModel>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.AxleCount).IsEqualTo(1);
            await Assert.That(station.Location).IsNull();
            await Assert.That(station.NameGerman).IsEqualTo("Darkehmen");
            await Assert.That(station.NameGermanNew).IsEqualTo("Angerapp");
            await Assert.That(station.NamePolish).IsNull();
            await Assert.That(station.NameRussian).IsEqualTo("Озёрск");
            await Assert.That(station.Name).IsEqualTo("Озёрск");
        }
    }

    [Test]
    public async Task StationCreated_WithNamingPolicyInEffectAtCreationTime_UsesThatPolicyForName()
    {
        await this.fixture.ResetAsync();
        var stationId = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, Who));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, Who, NamingPolicy.German));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(stationId, new StationCreated(stationId, Who, "Lublinitz", "Loben", "Lubliniec", null));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
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
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var station = await session.LoadAsync<StationReadModel>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.Location).IsEqualTo(location);
            await Assert.That(station.AxleCount).IsEqualTo(1);
            await Assert.That(station.Name).IsEqualTo("Lubliniec");
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
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var station = await session.LoadAsync<StationReadModel>(stationId);
            await Assert.That(station).IsNotNull();
            await Assert.That(station.AxleCount).IsEqualTo(1 + axlesAdded);
        }
    }

    [Test]
    public async Task SettingsNamingPolicySet_RecomputesNameOnExistingStationEachTimePolicyChanges()
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
            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var afterGerman = await session.LoadAsync<StationReadModel>(stationId);
            await Assert.That(afterGerman).IsNotNull();
            await Assert.That(afterGerman.Name).IsEqualTo("Loben");
            await Assert.That(afterGerman.AxleCount).IsEqualTo(1);
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, Who, NamingPolicy.Modern));
            await session.SaveChangesAsync();

            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var afterModern = await session.LoadAsync<StationReadModel>(stationId);
            await Assert.That(afterModern).IsNotNull();
            await Assert.That(afterModern.Name).IsEqualTo("Lubliniec");
            await Assert.That(afterModern.AxleCount).IsEqualTo(1);
        }
    }

    [Test]
    public async Task SettingsNamingPolicySet_UpdatesAllExistingStationsIndependently()
    {
        await this.fixture.ResetAsync();
        var stationId1 = Guid.NewGuid();
        var stationId2 = Guid.NewGuid();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, Who));
            session.Events.Append(stationId1, new StationCreated(stationId1, Who, "Lublinitz", "Loben", "Lubliniec", null));
            session.Events.Append(stationId2, new StationCreated(stationId2, Who, "Gerdauen", null, null, "Железнодорожный"));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, Who, NamingPolicy.German));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var station1 = await session.LoadAsync<StationReadModel>(stationId1);
            var station2 = await session.LoadAsync<StationReadModel>(stationId2);

            await Assert.That(station1).IsNotNull();
            await Assert.That(station2).IsNotNull();
            await Assert.That(station1.Name).IsEqualTo("Loben");
            await Assert.That(station2.Name).IsEqualTo("Gerdauen");
        }
    }

    [Test]
    public async Task SettingsNamingPolicySet_WithNoStationsCreatedYet_CompletesWithoutError()
    {
        await this.fixture.ResetAsync();

        using (var session = this.fixture.Store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, Who));
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, Who, NamingPolicy.German));
            await session.SaveChangesAsync();
        }

        using (var session = this.fixture.Store.LightweightSession())
        {
            await this.fixture.Daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);

            var missingStation = await session.Query<StationReadModel>().ToListAsync(CancellationToken.None);
            await Assert.That(missingStation).IsEmpty();
        }
    }
}