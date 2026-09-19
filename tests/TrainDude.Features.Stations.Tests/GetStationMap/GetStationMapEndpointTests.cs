// <copyright file="GetStationMapEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests.GetStationMap;

using System.Threading.Tasks;

public class GetStationMapEndpointTests
{
    [Test]
    public async Task Get_StationWithLocation_ReturnsSingleLocationAndNoEdges()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Get_StationWithoutLocation_ReturnsEmptyLocationsAndNoEdges()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Get_NonExistentId_Returns404()
    {
        // TODO
        Assert.Fail("TODO");
    }
}