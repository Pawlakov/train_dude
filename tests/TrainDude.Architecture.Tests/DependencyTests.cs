namespace TrainDude.Architecture.Tests;

using NetArchTest.Rules;

using Xunit;

public class DependencyTests
{
    [Fact]
    public void ClientShouldNotDependOnServer()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("TrainDude.Web.Program")
            .GetResult();

        Assert.True(result.IsSuccessful, "Client should not depend on server-side assemblies.");
    }

    [Fact]
    public void ClientShouldNotDependOnInfrastructure()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("TrainDude.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful, "Client should not depend on server-side assemblies.");
    }

    [Fact]
    public void ClientShouldNotDependOnFeatures()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("TrainDude.Features.Shared.SettingsSingleton")
            .GetResult();

        Assert.True(result.IsSuccessful, "Client should not depend on server-side assemblies.");
    }

    [Fact]
    public void ClientShouldDependOnFeatureContracts()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .Should()
            .HaveDependencyOnAll("TrainDude.Features.Shared.Contracts.Values")
            .GetResult();

        Assert.True(result.IsSuccessful, "Client should should depend on feature contracts.");
    }
}