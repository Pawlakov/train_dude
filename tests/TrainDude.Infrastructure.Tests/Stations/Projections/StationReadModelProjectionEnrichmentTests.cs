// <copyright file="StationReadModelProjectionEnrichmentTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Stations.Projections;

using System.Threading.Tasks;

public class StationReadModelProjectionEnrichmentTests
{
    [Test]
    public async Task EnrichEventsAsync_NoSettingsDocument_DefaultsToModernPolicy()
    {
        // TODO No SharedSettingsReference yet → resolves under NamingPolicy.Modern (default).
        Assert.Fail("TODO");
    }

    [Test]
    public async Task EnrichEventsAsync_SettingsDocumentPresent_UsesStoredPolicy()
    {
        // TODO SharedSettingsReference present → resolves under that stored policy.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task EnrichEventsAsync_ResolvedName_MatchesNameResolverOutputDirectly()
    {
        // TODO Enriched Name matches NameResolver.GetNameSelector(policy) output directly (no drift between projection and resolver).
        Assert.Fail("TODO");
    }

    [Test]
    public async Task EnrichEventsAsync_DuplicateStationCreatedInSlice_Throws()
    {
        // TODO More than one StationCreated in a slice → .SingleOrDefault() throws; assert this failure mode explicitly.
        Assert.Fail("TODO");
    }

    [Test]
    public async Task EnrichEventsAsync_SliceWithoutStationCreated_SkipsEnrichment()
    {
        // TODO A slice with only a SettingsNamingPolicySet (no StationCreated) → enrichment skipped cleanly, no exception.
        Assert.Fail("TODO");
    }
}