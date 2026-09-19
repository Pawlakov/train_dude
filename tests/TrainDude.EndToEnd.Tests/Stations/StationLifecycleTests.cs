// <copyright file="StationLifecycleTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.EndToEnd.Tests.Stations;

using System.Threading.Tasks;

public class StationLifecycleTests
{
    [Test]
    public async Task CreateThenGet_ReflectsInitialState_AfterProjectionCatchesUp()
    {
        // TODO Create → GetStation reflects initial state (name under active policy, axle count 1, no location).
        Assert.Fail("TODO");
    }

    [Test]
    public async Task CreateThenAddAxleMultipleTimes_ThenGet_ReflectsAccumulatedCount()
    {
        // TODO Create → AddAxle ×N → GetStation reflects 1 + N.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task CreateThenSetLocation_ThenGetAndGetMap_ReflectLocation()
    {
        // TODO Create → SetLocation → both GetStation and GetStationMap reflect it.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task CreateTwoStations_ThenGetStations_ReflectsFlagsAndOrdering()
    {
        // TODO Two stations (one located, one not) → GetStations shows correct HasLocation flags and ordering.
        Assert.Fail("TODO");
    }
}