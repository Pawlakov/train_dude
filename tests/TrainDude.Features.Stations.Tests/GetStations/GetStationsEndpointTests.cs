// <copyright file="GetStationsEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.GetStations;

using System;
using System.Linq;
using System.Threading.Tasks;

using Marten;
using Marten.Events;

using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.GetStations;
using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Stations.Domain.Events;

using TUnit.Core.Services;

[NotInParallel]
public class GetStationsEndpointTests
    : BaseEndpointTests
{
    [Before(Test)]
    public async Task SetUp()
    {
        await this.AnonFixture.Host.ResetAllMartenDataAsync();
        await this.AuthFixture.Host.ResetAllMartenDataAsync();
    }

    [Test]
    public async Task Get_NoDocuments_Returns200AndEmptyList()
    {
        var getResult = await this.AuthFixture.Host.Scenario(x =>
        {
            x.Get.Json(new GetStationsQuery()).ToUrl(GetStationsQuery.Route);
            x.StatusCodeShouldBe(200);
        });

        var response = await getResult.ReadAsJsonAsync<GetStationsQueryResponse>();

        await Assert.That(response.Items).IsEmpty();
    }

    [Test]
    public async Task Get_MultipleDocuments_OrderedByName()
    {
        // TODO [Theory] across diacritics/case
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Get_MixedLocatedAndUnlocatedStations_ReflectsHasLocationFlag()
    {
        var station1Id = Guid.NewGuid();
        var station2Id = Guid.NewGuid();
        var station3Id = Guid.NewGuid();
        var store = this.AuthFixture.Host.Services.GetRequiredService<IDocumentStore>();
        await using (var session = store.LightweightSession())
        {
            session.Events.StartStream<StationAggregate>(station1Id, new StationCreated(station1Id, this.AuthFixture.AuthenticatedActor, "German", null, "Polish", null));
            session.Events.StartStream<StationAggregate>(station2Id, new StationCreated(station2Id, this.AuthFixture.AuthenticatedActor, "German", null, "Polish", null));
            session.Events.StartStream<StationAggregate>(station3Id, new StationCreated(station3Id, this.AuthFixture.AuthenticatedActor, "German", null, "Polish", null));

            session.Events.Append(station2Id, new StationLocationSet(station2Id, this.AuthFixture.AuthenticatedActor, new Location(30, 40)));

            await session.SaveChangesAsync();
        }

        await store.WaitForNonStaleProjectionDataAsync(TimeSpan.FromSeconds(30));

        var getResult = await this.AuthFixture.Host.Scenario(x =>
        {
            x.Get.Json(new GetStationsQuery()).ToUrl(GetStationsQuery.Route);
            x.StatusCodeShouldBe(200);
        });

        var response = await getResult.ReadAsJsonAsync<GetStationsQueryResponse>();

        await Assert.That(response.Items).Count().IsEqualTo(3);
        await Assert.That(response.Items.Where(x => x.HasLocation)).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Get_EachItem_MapsStationIdAndNameFromDocument()
    {
        // TODO
        Assert.Fail("TODO");
    }
}