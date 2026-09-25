// <copyright file="AddAxleEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests.AddAxle;

using System;
using System.Linq;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Stations.Contracts.AddAxle;
using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

[NotInParallel]
public class AddAxleEndpointTests
    : BaseEndpointTests
{
    [Before(Test)]
    public async Task SetUp()
    {
        await this.AnonFixture.Host.ResetAllMartenDataAsync();
        await this.AuthFixture.Host.ResetAllMartenDataAsync();
    }

    [Test]
    public async Task Post_ExistingStation_Returns200AndAppendsEvent()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        await this.PostUpdateCommandAsync(new AddAxleCommand(stationId, 1));

        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();

        await using var session = store.QuerySession();

        var events = await session.Events
            .QueryAllRawEvents()
            .ToListAsync();

        await Assert.That(events).Count().IsEqualTo(2);
        var axleAdded = (StationAxleAdded?)events.Last().Data;
        await Assert.That(axleAdded).IsNotNull();

        await Assert.That(axleAdded.StationId).IsEqualTo(stationId);
        await Assert.That(axleAdded.Who).IsEqualTo(this.AuthFixture.AuthenticatedActor);
    }

    [Test]
    public async Task Post_NonExistentStation_Returns404()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var sequence = await this.GetCurrentEventSequenceAsync();

        var stationId = Guid.NewGuid();
        await this.PostUpdateCommandAsync(new AddAxleCommand(stationId, 1), 404);

        await this.AssertNothingWrittenAsync(sequence);
    }

    [Test]
    [Arguments("")]
    [Arguments("{ invalid json")]
    [Arguments("{ \"Valid\": \"syntactically but not semantically\" }")]
    [Arguments("{ \"Id\": \"surely not a guid\" }")]
    public async Task Post_MalformedId_Returns400(string text)
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var sequence = await this.GetCurrentEventSequenceAsync();
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        var route = AddAxleCommand.Route.Replace("{id}", stationId.ToString());
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

        await this.PostUpdateCommandAsync(new AddAxleCommand(stationId, 1), 302, false);

        await this.AssertNothingWrittenAsync(sequence);
    }

    [Test]
    public async Task Post_ConcurrentCalls_Returns200Then409()
    {
        var createResult = await this.PostCreateCommandAsync(new CreateStationCommand("German", null, "Polish", null));
        var created = await createResult.ReadAsJsonAsync<CreatedResponse>();
        var stationId = created.Id;

        await this.PostUpdateCommandAsync(new AddAxleCommand(stationId, 1));
        await this.PostUpdateCommandAsync(new AddAxleCommand(stationId, 1), 409);

        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();

        await using var session = store.QuerySession();

        var events = await session.Events
            .QueryAllRawEvents()
            .OrderBy(x => x.Sequence)
            .ToListAsync();

        await Assert.That(events).Count().IsEqualTo(2);

        var axleAdded = events
            .Where(x => x.Data is StationAxleAdded)
            .ToList();

        await Assert.That(axleAdded).Count().IsEqualTo(1);
    }
}