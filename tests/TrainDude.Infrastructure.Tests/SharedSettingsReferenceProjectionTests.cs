// <copyright file="SharedSettingsReferenceProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests;

using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.ReadModels;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerClass)]
public class SharedSettingsReferenceProjectionTests
{
    private readonly ProjectionStoreFixture fixture;

    public SharedSettingsReferenceProjectionTests(ProjectionStoreFixture fixture)
    {
        this.fixture = fixture;
    }

    [Test]
    public async Task SettingsCreated_DefaultsToModernNamingPolicy()
    {
        await this.fixture.ResetAsync();
        var who = "test@example.com";

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, who));
        await session.SaveChangesAsync();

        await this.fixture.Daemon.RebuildProjectionAsync<SharedSettingsReference>(CancellationToken.None);

        var settings = await session.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id);
        await Assert.That(settings).IsNotNull();
        await Assert.That(settings.NamingPolicy).IsEqualTo(NamingPolicy.Modern);
    }

    [Test]
    public async Task SettingsNamingPolicySet_UpdatesNamingPolicy()
    {
        await this.fixture.ResetAsync();
        var who = "test@example.com";

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, who));
        session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.German));
        await session.SaveChangesAsync();

        await this.fixture.Daemon.RebuildProjectionAsync<SharedSettingsReference>(CancellationToken.None);

        var settings = await session.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id);
        await Assert.That(settings).IsNotNull();
        await Assert.That(settings.NamingPolicy).IsEqualTo(NamingPolicy.German);
    }

    [Test]
    public async Task SettingsNamingPolicySet_CanRevertToAPreviousPolicy()
    {
        await this.fixture.ResetAsync();
        var who = "test@example.com";

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(SettingsSingleton.Id, new SettingsCreated(SettingsSingleton.Id, who));
        session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.German));
        session.Events.Append(SettingsSingleton.Id, new SettingsNamingPolicySet(SettingsSingleton.Id, who, NamingPolicy.Modern));
        await session.SaveChangesAsync();

        await this.fixture.Daemon.RebuildProjectionAsync<SharedSettingsReference>(CancellationToken.None);

        var settings = await session.LoadAsync<SharedSettingsReference>(SettingsSingleton.Id);
        await Assert.That(settings).IsNotNull();
        await Assert.That(settings.NamingPolicy).IsEqualTo(NamingPolicy.Modern);
    }
}