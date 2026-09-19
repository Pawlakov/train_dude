// <copyright file="StationReadModelProjectionApplyTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Stations.Projections;

using System.Threading.Tasks;

public class StationReadModelProjectionApplyTests
{
    [Test]
    public async Task Apply_StationCreatedWithReferences_SetsIdAxleCountNamesAndResolvedName()
    {
        // TODO Apply(StationCreatedWithReferences): Id, AxleCount = 1, raw name fields, and Name all set correctly.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_StationLocationSet_UpdatesLocationOnly()
    {
        // TODO Apply(StationLocationSet) / Apply(StationAxleAdded) match the domain aggregate's own behavior (overwrite / +1 accumulate).
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_StationAxleAdded_IncrementsAxleCountByOne()
    {
        // TODO Apply(SettingsNamingPolicySet) recomputes Name from the read model's own current fields, leaves AxleCount/Location/raw names untouched.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_SettingsNamingPolicySet_RecomputesNameFromReadModelFields()
    {
        // TODO Apply(SettingsNamingPolicySet) is idempotent under repeated application.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task Apply_SettingsNamingPolicySet_IsIdempotentUnderRepeatedApplication()
    {
        // TODO Cross-check: name resolution from a StationCreated event vs. from a StationReadModel (both IHasAlternativeNames) agree given identical field values.
        // TODO Regression guard: "axle count starts at 1" is asserted consistently for both StationAggregate and the projection, failing if only one side changes.
        Assert.Fail("TODO");
    }
}