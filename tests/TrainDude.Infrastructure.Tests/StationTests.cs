namespace TrainDude.Infrastructure.Tests;

using System;
using System.Threading;
using System.Threading.Tasks;

using JasperFx;
using JasperFx.Events.Projections;

using Marten;

using Microsoft.Extensions.Configuration;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.ReadModels;
using TrainDude.Infrastructure.Settings.Projections;
using TrainDude.Infrastructure.Stations.Projections;

using Xunit;

public class Tests
{
    [Fact]
    public async Task Test()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets("0d6c2b99-3ac6-4582-b9c1-04c3013a5c88")
            .Build();

        using var store = DocumentStore.For(options =>
        {
            options.Connection(config.GetConnectionString("Write"));
            options.DatabaseSchemaName = "train_dude_test";

            options.Projections.Add<SharedSettingsReferenceProjection>(ProjectionLifecycle.Async);
            options.Projections.Add<StationReadModelProjection>(ProjectionLifecycle.Async);

            options.AutoCreateSchemaObjects = AutoCreate.All;
        });

        await store.Advanced.Clean.DeleteAllEventDataAsync();
        await store.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(StationReadModel));

        using var daemon = await store.BuildProjectionDaemonAsync();

        var who = "test@example.com";
        var stationId = Guid.NewGuid();
        var location = new Location(20, 50);

        using (var session = store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, who));
            await session.SaveChangesAsync();

            await daemon.RebuildProjectionAsync<SharedSettingsReference>(CancellationToken.None);
            var settings = await session.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id);
            Assert.NotNull(settings);
            Assert.Equal(NamingPolicy.Modern, settings.NamingPolicy);
        }

        using (var session = store.LightweightSession())
        {
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.German));
            session.Events.Append(stationId, new StationCreated(stationId, who, "Lublinitz", "Loben", "Lubliniec", null));
            await session.SaveChangesAsync();

            await daemon.RebuildProjectionAsync<SharedSettingsReference>(CancellationToken.None);
            var settings = await session.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id);
            Assert.NotNull(settings);
            Assert.Equal(NamingPolicy.German, settings.NamingPolicy);

            await daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);
            var station = await session.LoadAsync<StationReadModel>(stationId);
            Assert.NotNull(station);
            Assert.Equal(1, station.AxleCount);
            Assert.Null(station.Location);
            Assert.Equal("Lublinitz", station.NameGerman);
            Assert.Equal("Loben", station.NameGermanNew);
            Assert.Equal("Lubliniec", station.NamePolish);
            Assert.Equal("Loben", station.Name);
        }

        using (var session = store.LightweightSession())
        {
            session.Events.Append(stationId, new StationLocationSet(stationId, who, location));
            session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.Modern));
            await session.SaveChangesAsync();

            await daemon.RebuildProjectionAsync<SharedSettingsReference>(CancellationToken.None);
            var settings = await session.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id);
            Assert.NotNull(settings);
            Assert.Equal(NamingPolicy.Modern, settings.NamingPolicy);

            await daemon.RebuildProjectionAsync<StationReadModel>(CancellationToken.None);
            var station = await session.LoadAsync<StationReadModel>(stationId);
            Assert.NotNull(station);
            Assert.Equal(1, station.AxleCount);
            Assert.Equal(location, station.Location);
            Assert.Equal("Lublinitz", station.NameGerman);
            Assert.Equal("Loben", station.NameGermanNew);
            Assert.Equal("Lubliniec", station.NamePolish);
            Assert.Equal("Lubliniec", station.Name);
        }
    }
}