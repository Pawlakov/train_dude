// <copyright file="DependencyTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Architecture.Tests;

using System.Threading.Tasks;

using NetArchTest.Rules;

public class DependencyTests
{
    [Test]
    public async Task ClientShouldNotDependOnServer()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("TrainDude.Web.Program")
            .GetResult();

        await Assert.That(result.IsSuccessful).IsTrue().Because("Client should not depend on server-side assemblies.");
    }

    [Test]
    public async Task ClientShouldNotDependOnInfrastructure()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("TrainDude.Infrastructure")
            .GetResult();

        await Assert.That(result.IsSuccessful).IsTrue().Because("Client should not depend on server-side assemblies.");
    }

    [Test]
    public async Task ClientShouldNotDependOnFeatures()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("TrainDude.Features.Shared.SettingsSingleton")
            .GetResult();

        await Assert.That(result.IsSuccessful).IsTrue().Because("Client should not depend on server-side assemblies.");
    }

    [Test]
    public async Task ClientShouldDependOnFeatureContracts()
    {
        var clientAssembly = typeof(TrainDude.Web.Client.Program).Assembly;

        var result = Types
            .InAssembly(clientAssembly)
            .Should()
            .HaveDependencyOnAll("TrainDude.Features.Shared.Contracts.Values")
            .GetResult();

        await Assert.That(result.IsSuccessful).IsTrue().Because("Client should should depend on feature contracts.");
    }
}