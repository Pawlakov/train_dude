// <copyright file="StationNamingPolicyPropagationTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.EndToEnd.Tests.Stations;

using System.Threading.Tasks;

public class StationNamingPolicyPropagationTests
{
    [Test]
    public async Task ChangeNamingPolicyViaSettings_WithNoStationsCommand_UpdatesExistingStationNames()
    {
        // TODO New, cross-feature: change the naming policy via the Settings feature with no command against Stations at all → GetStation/GetStations for pre-existing stations reflect the new Name, proving the fan-out end-to-end.
        Assert.Fail("TODO");
    }
}