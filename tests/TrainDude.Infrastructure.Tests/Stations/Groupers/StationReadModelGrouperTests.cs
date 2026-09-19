// <copyright file="StationReadModelGrouperTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Stations.Groupers;

using System.Threading.Tasks;

public class StationReadModelGrouperTests
{
    [Test]
    public async Task Group_StationEvents_RouteToOwnStationSlice()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Group_EventsForTwoStations_KeepSlicesIsolated()
    {
        // TODO IStationEvents route to their own station's slice; two stations in one batch stay isolated.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Group_NamingPolicySet_FansOutToAllExistingStationIds()
    {
        // TODO A SettingsNamingPolicySet event fans out to every existing station id, not just ones with other events in the batch.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Group_NamingPolicySet_WithNoStations_ProducesNoGroups()
    {
        // TODO Zero stations at the time of a policy change → no groups, no exception.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Group_NamingPolicySet_WithNStations_ProducesExactlyNGroups()
    {
        // TODO N stations → exactly N groups for one policy-change event.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Group_NewStationAndNamingPolicySetInSameBatch_DeterminesInclusion()
    {
        // TODO Race case: a StationCreated and a SettingsNamingPolicySet in the same batch — does the brand-new station also receive the policy event, given stationIds is queried live inside Group?
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Group_MultipleHistoricalPolicyChanges_AllStationsConvergeOnFinalPolicy()
    {
        // TODO Stations created before and after several historical policy changes all converge to the final policy after full replay.
        Assert.Fail("TODO");
    }
}