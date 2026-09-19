// <copyright file="StationsValidationProblemDetailsTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests.Shared;

using System.Threading.Tasks;

public class StationsValidationProblemDetailsTests
{
    [Test]
    public async Task InvalidCommand_Always_ReturnsConsistentProblemDetailsShape()
    {
        // TODO [Theory] over CreateStation/SetLocation invalid payloads
        Assert.Fail("TODO");
    }
}