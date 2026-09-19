// <copyright file="SetLocationEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.SetLocation;

using System.Threading.Tasks;

public class SetLocationEndpointTests
{
    [Test]
    public async Task Post_ValidLocation_Returns200AndAppendsEvent()
    {
        // TODO
        Assert.Fail("TODO");
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