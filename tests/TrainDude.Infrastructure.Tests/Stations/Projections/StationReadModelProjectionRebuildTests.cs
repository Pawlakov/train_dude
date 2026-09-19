// <copyright file="StationReadModelProjectionRebuildTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Stations.Projections;

using System.Threading.Tasks;

// (heavier — real Postgres, daemon rebuild)
public class StationReadModelProjectionRebuildTests
{
    [Test]
    public async Task Project_StationCreatedOnly_ProducesExpectedInitialReadModel()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Project_FullCommandSequence_ProducesExpectedFinalReadModel()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Project_InterleavedEventsAcrossStations_KeepsReadModelsIsolated()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Project_NamingPolicySetAfterStationsExist_UpdatesAllExistingStations()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Project_StationCreatedAfterNamingPolicySet_ResolvesUnderLatestPolicy()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task RebuildFromScratch_MatchesLiveIncrementalProcessing()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task RebuildFromEmptyStore_WithDelayedSettingsDocument_StillResolvesCorrectNames()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Project_BurstOfAxleAddedInSingleBatch_DropsNoEvents()
    {
        // TODO
        Assert.Fail("TODO");
    }
}