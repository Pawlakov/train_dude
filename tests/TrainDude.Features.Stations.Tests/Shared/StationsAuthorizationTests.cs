// <copyright file="StationsAuthorizationTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests.Shared;

using System.Threading.Tasks;

public class StationsAuthorizationTests
{
    [Test]
    public async Task ProtectedEndpoints_Unauthenticated_Return401()
    {
        // TODO [Theory] over all non-AllowAnonymous routes
        Assert.Fail("TODO");
    }
}