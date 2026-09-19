// <copyright file="StationsConcurrencyTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.EndToEnd.Tests.Stations;

using System.Threading.Tasks;

public class StationsConcurrencyTests
{
    [Test]
    public async Task ConcurrentAddAxleCalls_ReflectAllSuccessfulCallsInReadModel()
    {
        // TODO Concurrent AddAxle calls via real HTTP → final AxleCount in the read model matches the number of successful calls.
        Assert.Fail("TODO");
    }
}