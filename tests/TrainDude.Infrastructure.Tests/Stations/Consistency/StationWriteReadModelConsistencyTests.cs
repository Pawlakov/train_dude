// <copyright file="StationWriteReadModelConsistencyTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Tests.Stations.Consistency;

using System.Threading.Tasks;

// TODO (references both TrainDude.Features.Stations and the projection — lives here since this project already depends on the feature project)
public class StationWriteReadModelConsistencyTests
{
    [Test]
    public async Task NameResolution_FromEventVersusFromReadModel_ProducesIdenticalResult()
    {
        // TODO
        Assert.Fail("TODO");
    }

    [Test]
    public async Task InitialAxleCount_AgreesBetweenAggregateAndProjection()
    {
        // TODO
        Assert.Fail("TODO");
    }
}