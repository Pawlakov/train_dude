// <copyright file="SetLocationEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.SetLocation;

using System.Linq;
using System.Threading.Tasks;

using Alba;

using Marten;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

[NotInParallel]
public class SetLocationEndpointTests
{
    [ClassDataSource<AuthenticatedHostFixture>(Shared = SharedType.PerTestSession)]
    public required AuthenticatedHostFixture AuthFixture { get; init; }

    [ClassDataSource<AnonymousHostFixture>(Shared = SharedType.PerTestSession)]
    public required AnonymousHostFixture AnonFixture { get; init; }

    [Test]
    public async Task Post_Valid_Returns200AndAppendsEvent()
    {
        var createResult = await this.AuthFixture.Host.Scenario(x =>
        {
            x.Post.Json(new CreateStationCommand("German", null, "Polish", null)).ToUrl(CreateStationCommand.Route);
            x.StatusCodeShouldBe(201);
        });

        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        var setLocationRoute = SetLocationCommand.Route.Replace("{id}", stationId.ToString());
        var setLocationResult = await this.AuthFixture.Host.Scenario(x =>
        {
            x.Post.Json(new SetLocationCommand(stationId, new Location(20, 50))).ToUrl(setLocationRoute);
            x.StatusCodeShouldBe(200);
        });

        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();

        await using var session = store.QuerySession();

        var events = await session.Events
            .QueryAllRawEvents()
            .ToListAsync();

        await Assert.That(events).Count().IsEqualTo(2);
        var locationSet = (StationLocationSet?)events.Last().Data;
        await Assert.That(locationSet).IsNotNull();

        await Assert.That(locationSet.StationId).IsEqualTo(stationId);
        await Assert.That(locationSet.Who).IsEqualTo(this.AuthFixture.AuthenticatedActor);
        await Assert.That(locationSet.Location).IsEqualTo(new Location(20, 50));
    }

    [Test]
    public async Task Post_DefaultLocation_Returns400()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_MissingLocationInBody_BindsToDefaultAndReturns400()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_NonExistentStation_Returns404()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_CalledTwice_LastLocationWins()
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

    [Test]
    public async Task Post_ConcurrentCallsOnSameStation_DoNotLoseAnUpdate()
    {
        // TODO
        Assert.Fail("TODO");
    }
}