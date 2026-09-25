// <copyright file="SetLocationEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.SetLocation;

using System;
using System.Linq;
using System.Threading.Tasks;

using Alba;

using Marten;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

[NotInParallel]
public class SetLocationEndpointTests
    : BaseEndpointTests
{
    [Before(Test)]
    public async Task SetUp()
    {
        await this.AnonFixture.Host.ResetAllMartenDataAsync();
        await this.AuthFixture.Host.ResetAllMartenDataAsync();
    }

    [Test]
    public async Task Post_Valid_Returns200AndAppendsEvent()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        await this.PostUpdateCommandAsync(new SetLocationCommand(stationId, 1, new Location(20, 50)));

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
    public async Task Post_DefaultLocation_Returns422()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;
        var sequence = await this.GetCurrentEventSequenceAsync();

        var setLocationResult = await this.PostUpdateCommandAsync(new SetLocationCommand(stationId, 1, default), 422);

        var problem = await setLocationResult.ReadAsJsonAsync<ValidationProblemDetails>();
        await Assert.That(problem.Errors).ContainsKey(nameof(SetLocationCommand.Location));
        await Assert.That(problem.Errors).AllKeys(x => x is nameof(SetLocationCommand.Location));
        await this.AssertNothingWrittenAsync(sequence);
    }

    [Test]
    public async Task Post_NonExistentStation_Returns404()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var sequence = await this.GetCurrentEventSequenceAsync();

        var stationId = Guid.NewGuid();
        await this.PostUpdateCommandAsync(new SetLocationCommand(stationId, 1, new Location(20, 50)), 404);

        await this.AssertNothingWrittenAsync(sequence);
    }

    [Test]
    [Arguments("")]
    [Arguments("{ invalid json")]
    [Arguments("{ \"Valid\": \"syntactically but not semantically\" }")]
    [Arguments("{ \"Location\": \"lokacja zmyślona\" }")]
    public async Task Post_WithMalformedJson_Returns400(string text)
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var sequence = await this.GetCurrentEventSequenceAsync();
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        var route = SetLocationCommand.Route.Replace("{id}", stationId.ToString());
        await this.AuthFixture.Host.Scenario(x =>
        {
            x.Post.Text(text).ContentType("application/json").ToUrl(route);
            x.StatusCodeShouldBe(400);
        });

        await this.AssertNothingWrittenAsync(sequence);
    }

    [Test]
    public async Task Post_Unauthenticated_Returns302()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var sequence = await this.GetCurrentEventSequenceAsync();
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        await this.PostUpdateCommandAsync(new SetLocationCommand(stationId, 1, new Location(20, 50)), 302, false);

        await this.AssertNothingWrittenAsync(sequence);
    }

    [Test]
    public async Task Post_ConcurrentCalls_Returns200Then409()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        var firstLocation = new Location(20, 50);
        var secondLocation = new Location(-10, 100);

        await this.PostUpdateCommandAsync(new SetLocationCommand(stationId, 1, firstLocation));
        await this.PostUpdateCommandAsync(new SetLocationCommand(stationId, 1, secondLocation), 409);

        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();

        await using var session = store.QuerySession();

        var events = await session.Events
            .QueryAllRawEvents()
            .OrderBy(x => x.Sequence)
            .ToListAsync();

        await Assert.That(events).Count().IsEqualTo(2);

        var locationsSet = events
            .Where(x => x.Data is StationLocationSet)
            .Select(x => ((StationLocationSet)x.Data!).Location)
            .ToList();

        await Assert.That(locationsSet).Count().IsEqualTo(1);
        await Assert.That(locationsSet.Count(x => x == firstLocation)).IsEqualTo(1);
    }
}