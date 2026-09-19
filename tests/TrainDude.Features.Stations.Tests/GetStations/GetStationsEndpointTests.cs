// <copyright file="GetStationsEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests.GetStations;

using System.Threading.Tasks;

public class GetStationsEndpointTests
{
    [Test]
    public async Task Get_NoDocuments_ReturnsEmptyList()
    {
        // TODO
        Assert.Fail("TODO");
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
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Get_EachItem_MapsStationIdAndNameFromDocument()
    {
        // TODO
        Assert.Fail("TODO");
    }
}