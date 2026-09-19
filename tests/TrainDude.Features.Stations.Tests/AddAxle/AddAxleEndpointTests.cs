// <copyright file="AddAxleEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests.AddAxle;

using System.Threading.Tasks;

public class AddAxleEndpointTests
{
    [Test]
    public async Task Post_ExistingStation_Returns200AndAppendsEvent()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_RepeatedCalls_AccumulateAxleCountOnStream()
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
    public async Task Post_MalformedId_Returns400()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Post_Always_StampsWhoFromAuthenticatedUser()
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