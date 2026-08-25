namespace TrainDude.Architecture.Tests;

using NetArchTest.Rules;

using Xunit;

public class DependencyTests
{
    [Fact]
    public void DomainShouldNotDepend()
    {
        var domainAssembly = typeof(TrainDude.Domain.Stations.StationAggregate).Assembly;

        var result = Types
            .InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("TrainDude.Queries.Data", "TrainDude.Queries.Contracts", "TrainDude.Web")
            .GetResult();

        Assert.True(result.IsSuccessful, "Domain should not depend on higher layers.");
    }
}