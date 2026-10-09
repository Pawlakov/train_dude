// <copyright file="GetStationEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.GetStation;

using System;
using System.Threading.Tasks;

using Marten;
using Marten.Events;

using TrainDude.Features.Settings;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared.Contracts.Enums;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.GetStation;
using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

[NotInParallel]
public class GetStationEndpointTests
    : BaseEndpointTests
{
    [Before(Test)]
    public async Task SetUp()
    {
        await this.AnonFixture.Host.ResetAllMartenDataAsync();
        await this.AuthFixture.Host.ResetAllMartenDataAsync();
    }

    [Test]
    [Arguments("German New", "Polish", null, false, null, "Polish")]
    [Arguments("German New", null, null, false, null, "???")]
    [Arguments("German New", null, "Russian", false, null, "Russian")]
    [Arguments(null, null, "Russian", false, null, "Russian")]
    [Arguments(null, null, null, false, null, "???")]
    [Arguments(null, "Polish", null, false, null, "Polish")]
    [Arguments(null, "Polish", null, true, null, "Polish")]
    [Arguments(null, "Polish", null, false, NamingPolicy.Modern, "Polish")]
    [Arguments(null, "Polish", null, false, NamingPolicy.German, "German")]
    public async Task Get_ExistingDocument_Returns200AndCurrentData(string? nameGermanNew, string? namePolish, string? nameRussian, bool hasLocation, NamingPolicy? policy, string expectedName)
    {
        Location? location = hasLocation ? new Location(20, 50) : null;
        var stationId = Guid.NewGuid();
        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();
        await using (var session = store.LightweightSession())
        {
            session.Events.StartStream<StationAggregate>(stationId, new StationCreated(stationId, this.AuthFixture.AuthenticatedActor, "German", nameGermanNew, namePolish, nameRussian));
            if (hasLocation)
            {
                session.Events.Append(stationId, new StationLocationSet(stationId, this.AuthFixture.AuthenticatedActor, location.Value));
            }

            if (policy.HasValue)
            {
                session.Events.Append(SettingsAccessor.SingletonId, new SettingsNamingPolicySet(SettingsAccessor.SingletonId, this.AuthFixture.AuthenticatedActor, policy.Value));
            }

            await session.SaveChangesAsync();
        }

        await store.WaitForNonStaleProjectionDataAsync(TimeSpan.FromSeconds(30));

        var route = GetStationQuery.Route.Replace("{id}", stationId.ToString());
        var getResult = await this.AuthFixture.Host.Scenario(x =>
        {
            x.Get.Json(new GetStationQuery(stationId)).ToUrl(route);
            x.StatusCodeShouldBe(200);
        });

        var response = await getResult.ReadAsJsonAsync<GetStationQueryResponse>();

        await Assert.That(response.Name).IsEqualTo(expectedName);
        await Assert.That(response.Location).IsEqualTo(location);
        await Assert.That(response.AxleCount).IsEqualTo(1);
    }

    [Test]
    public async Task Get_NonExistentId_Returns404()
    {
        var stationId = Guid.NewGuid();
        var route = GetStationQuery.Route.Replace("{id}", stationId.ToString());
        await this.AuthFixture.Host.Scenario(x =>
        {
            x.Get.Json(new GetStationQuery(stationId)).ToUrl(route);
            x.StatusCodeShouldBe(404);
        });
    }
}