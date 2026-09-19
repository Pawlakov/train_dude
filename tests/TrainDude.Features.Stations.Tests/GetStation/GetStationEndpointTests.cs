// <copyright file="GetStationEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.GetStation;

using System.Threading.Tasks;

public class GetStationEndpointTests
{
    [Test]
    public async Task Get_ExistingDocument_MapsFieldsOneToOne()
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

    [Test]
    public async Task Get_MalformedId_Returns400()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Get_NullLocationInDocument_ReturnsNullLocation()
    {
        // TODO
        Assert.Fail("TODO");
    }
}