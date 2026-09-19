// <copyright file="StationsSmokeTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.EndToEnd.Tests.Stations;

using System.Threading.Tasks;

public class StationsSmokeTests
{
    [Test]
    public async Task FullJourney_CreateSetLocationAddAxlesFetchIndividualListAndBothMaps_Succeeds()
    {
        // TODO Broad user-journey smoke test: create, set location, add several axles, fetch individually, fetch in the list, fetch on both map endpoints, in one flow.
        Assert.Fail("TODO");
    }
}