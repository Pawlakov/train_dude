// <copyright file="StationAggregateTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests.Domain;

using System.Threading.Tasks;

public class StationAggregateTests
{
    [Test]
    public async Task Make_ValidCommand_ProducesStationCreatedEventWithSameData()
    {
        // TODO Make() produces StationCreated with all fields passed through unchanged, nulls preserved.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_StationCreated_SetsIdAndNameFields()
    {
        // TODO Apply(StationCreated) sets Id, AxleCount = 1, Location = null, and all four name fields.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_StationCreated_SetsAxleCountToOne()
    {
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_StationCreated_LeavesLocationNull()
    {
        Assert.Fail("TODO");
    }

    [Test]
    public async Task SetLocation_Always_ProducesEventWithoutMutatingState()
    {
        // TODO SetLocation() returns StationLocationSet with correct Id/who/location; doesn't mutate state itself.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_StationLocationSet_UpdatesLocationOnly()
    {
        // TODO Apply(StationLocationSet) updates Location only.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task AddAxle_Always_ProducesEventWithoutMutatingState()
    {
        // TODO AddAxle() returns StationAxleAdded with correct Id/who; doesn't mutate state itself.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_StationAxleAdded_IncrementsCountByOne()
    {
        // TODO [Theory], starting counts 1/2/5 Apply(StationAxleAdded) increments AxleCount by exactly 1, from various starting counts.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Replay_FullLifecycle_ProducesExpectedFinalState()
    {
        // TODO Full lifecycle replay: Created → LocationSet → AxleAdded ×N produces the expected final state.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Deserialize_ViaJsonConstructor_MatchesStateFromEventReplay()
    {
        // TODO Given/when/then decider tests for AddAxle and SetLocation (given prior events, when command, then expected event).
        // TODO JSON/snapshot round-trip: state via the [JsonConstructor] path matches state via parameterless-ctor + event replay.
        Assert.Fail("TODO");
    }
}