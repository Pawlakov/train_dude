// <copyright file="StationsErrorHandlingTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.EndToEnd.Tests.Stations;

using System.Threading.Tasks;

public class StationsErrorHandlingTests
{
    [Test]
    public async Task CommandsAgainstNonExistentStation_Return404EndToEnd()
    {
        // TODO AddAxle/SetLocation against a non-existent id → 404 through the whole st}ack.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task InvalidCreatePayload_Returns400WithClientFacingErrorShape()
    {
        // TODO Invalid CreateStation payload → 400 with the client-facing error shape, end-to-end.
        Assert.Fail("TODO");
    }
}