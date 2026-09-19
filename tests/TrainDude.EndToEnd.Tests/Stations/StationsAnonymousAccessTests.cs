// <copyright file="StationsAnonymousAccessTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.EndToEnd.Tests.Stations;

using System.Threading.Tasks;

public class StationsAnonymousAccessTests
{
    [Test]
    public async Task GetStationsMap_WithNoAuthToken_ReturnsBothLocations()
    {
        // TODO Two located stations → GetStationsMap called with no auth token → both returned.
        Assert.Fail("TODO");
    }
}