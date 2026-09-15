// <copyright file="SharedSettingsReferenceProjectionTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Settings;

using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Settings;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Infrastructure.Shared.ReadModels;

[NotInParallel]
[ClassDataSource<ProjectionStoreFixture>(Shared = SharedType.PerTestSession)]
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
        var who = "test@example.com";

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(SettingsAccessor.SingletonId, new SettingsCreated(SettingsAccessor.SingletonId, who));
        await session.SaveChangesAsync();

        var settings = await session.LoadAsync<SharedSettingsReference>(SettingsAccessor.SingletonId);
        await Assert.That(settings).IsNotNull();
        await Assert.That(settings.NamingPolicy).IsEqualTo(NamingPolicy.Modern);
    }

    [Test]
    public async Task SettingsNamingPolicySet_UpdatesNamingPolicy()
    {
        var who = "test@example.com";

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(SettingsAccessor.SingletonId, new SettingsCreated(SettingsAccessor.SingletonId, who));
        session.Events.Append(SettingsAccessor.SingletonId, new SettingsNamingPolicySet(SettingsAccessor.SingletonId, who, NamingPolicy.German));
        await session.SaveChangesAsync();

        var settings = await session.LoadAsync<SharedSettingsReference>(SettingsAccessor.SingletonId);
        await Assert.That(settings).IsNotNull();
        await Assert.That(settings.NamingPolicy).IsEqualTo(NamingPolicy.German);
    }

    [Test]
    public async Task SettingsNamingPolicySet_CanRevertToAPreviousPolicy()
    {
        var who = "test@example.com";

        using var session = this.fixture.Store.LightweightSession();
        session.Events.Append(SettingsAccessor.SingletonId, new SettingsCreated(SettingsAccessor.SingletonId, who));
        session.Events.Append(SettingsAccessor.SingletonId, new SettingsNamingPolicySet(SettingsAccessor.SingletonId, who, NamingPolicy.German));
        session.Events.Append(SettingsAccessor.SingletonId, new SettingsNamingPolicySet(SettingsAccessor.SingletonId, who, NamingPolicy.Modern));
        await session.SaveChangesAsync();

        var settings = await session.LoadAsync<SharedSettingsReference>(SettingsAccessor.SingletonId);
        await Assert.That(settings).IsNotNull();
        await Assert.That(settings.NamingPolicy).IsEqualTo(NamingPolicy.Modern);
    }
}