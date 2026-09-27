// <copyright file="GetStationMapEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.GetStationMap;

using System;
using System.Linq;
using System.Threading.Tasks;

using Marten;
using Marten.Events;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.GetStationMap;
using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

[NotInParallel]
public class GetStationMapEndpointTests
    : BaseEndpointTests
{
    [Before(Test)]
    public async Task SetUp()
    {
        await this.AnonFixture.Host.ResetAllMartenDataAsync();
        await this.AuthFixture.Host.ResetAllMartenDataAsync();
    }

    [Test]
    public async Task Get_StationWithLocation_Returns200AndSingleLocation()
    {
        var location = new Location(20, 50);
        var stationId = Guid.NewGuid();
        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();
        await using (var session = store.LightweightSession())
        {
            session.Events.StartStream<StationAggregate>(stationId, new StationCreated(stationId, this.AuthFixture.AuthenticatedActor, "German", null, "Polish", null));
            session.Events.Append(stationId, new StationLocationSet(stationId, this.AuthFixture.AuthenticatedActor, location));

            await session.SaveChangesAsync();
        }

        await store.WaitForNonStaleProjectionDataAsync(TimeSpan.FromSeconds(30));

        var route = GetStationMapQuery.Route.Replace("{id}", stationId.ToString());
        var getResult = await this.AuthFixture.Host.Scenario(x =>
        {
            x.Get.Json(new GetStationMapQuery(stationId)).ToUrl(route);
            x.StatusCodeShouldBe(200);
        });

        var response = await getResult.ReadAsJsonAsync<MapQueryResponse>();

        await Assert.That(response.StationPoints).Count().IsEqualTo(1);
        await Assert.That(response.SegmentLineStrings).IsEmpty();
        await Assert.That(response.StationPoints.Single()).IsEqualTo(location);
    }

    [Test]
    public async Task Get_StationWithoutLocation_Returns200AndEmptyLocations()
    {
        var stationId = Guid.NewGuid();
        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();
        await using (var session = store.LightweightSession())
        {
            session.Events.StartStream<StationAggregate>(stationId, new StationCreated(stationId, this.AuthFixture.AuthenticatedActor, "German", null, "Polish", null));

            await session.SaveChangesAsync();
        }

        await store.WaitForNonStaleProjectionDataAsync(TimeSpan.FromSeconds(30));

        var route = GetStationMapQuery.Route.Replace("{id}", stationId.ToString());
        var getResult = await this.AuthFixture.Host.Scenario(x =>
        {
            x.Get.Json(new GetStationMapQuery(stationId)).ToUrl(route);
            x.StatusCodeShouldBe(200);
        });

        var response = await getResult.ReadAsJsonAsync<MapQueryResponse>();

        await Assert.That(response.StationPoints).IsEmpty();
        await Assert.That(response.SegmentLineStrings).IsEmpty();
    }

    [Test]
    public async Task Get_NonExistentId_Returns404()
    {
        var stationId = Guid.NewGuid();
        var route = GetStationMapQuery.Route.Replace("{id}", stationId.ToString());
        await this.AuthFixture.Host.Scenario(x =>
        {
            x.Get.Json(new GetStationMapQuery(stationId)).ToUrl(route);
            x.StatusCodeShouldBe(404);
        });
    }
}