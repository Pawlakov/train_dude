// <copyright file="CreateStationEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.CreateStation;

using System;
using System.Linq;
using System.Threading.Tasks;

using Alba;

using Marten;
using Marten.Events;

using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

using Wolverine.Tracking;

[NotInParallel]
[ClassDataSource<HostFixture>(Shared = SharedType.PerTestSession)]
public class CreateStationEndpointTests
{
    private readonly HostFixture fixture;

    public CreateStationEndpointTests(HostFixture fixture)
    {
        this.fixture = fixture;
    }

    [Before(Test)]
    public async Task SetUp()
    {
        await this.fixture.Host.ResetAllMartenDataAsync();
    }

    [Test]
    [Arguments("Lublinitz", "Loben", "Lubliniec", null)]
    [Arguments("Gerdauen", null, null, "Железнодорожный")]
    public async Task CreateStation_WhenValid_CreatesStation(string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        var result = await this.fixture.Host.Scenario(x =>
        {
            x.Post.Json(new CreateStationCommand(nameGerman, nameGermanNew, namePolish, nameRussian)).ToUrl(CreateStationCommand.Route);
            x.StatusCodeShouldBe(201);
        });

        var store = this.fixture.Host.Services.GetRequiredService<IDocumentStore>();

        await using var session = store.QuerySession();

        var events = await session.Events
            .QueryAllRawEvents()
            .Where(x => x.EventTypesAre(typeof(StationCreated)))
            .ToListAsync();

        await Assert.That(events).Count().IsEqualTo(1);

        var stationCreated = (StationCreated)events.Single().Data;

        await Assert.That(stationCreated.NameGerman).IsEqualTo(nameGerman);
    }

    [Test]
    public async Task Post_ValidPayload_Returns201WithLocationHeaderAndId()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_ValidPayload_StartsNewEventStreamWithStationCreated()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_BothPolishAndRussianNamesProvided_Returns400()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_Always_StampsWhoFromAuthenticatedUser()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_Unauthenticated_Returns401()
    {
        // TODO
        Assert.Fail("TODO");
    }
}