// <copyright file="HostFixture.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Alba;
using Alba.Security;

using JasperFx.CommandLine;

using Testcontainers.PostgreSql;

using TrainDude.Web;

using TUnit.Core.Interfaces;

using Wolverine;

public class AuthenticatedHostFixture
    : IAsyncInitializer, IAsyncDisposable
{
    private readonly PostgreSqlContainer db = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .Build();

    public IAlbaHost Host { get; private set; }

    public string AuthenticatedActor => "Actor";

    public async Task InitializeAsync()
    {
        await this.db.StartAsync();

        var configValues = new Dictionary<string, string?> { ["ConnectionStrings:Write"] = this.db.GetConnectionString() };
        var configOverride = ConfigurationOverride.Create(configValues);

        var securityStub = new AuthenticationStub()
            .With(ClaimTypes.Email, "test@example.com")
            .With(ClaimTypes.NameIdentifier, this.AuthenticatedActor)
            .WithName("test");

        JasperFxEnvironment.AutoStartHost = true;

        this.Host = await AlbaHost.For<Program>(
        x =>
        {
            x.ConfigureServices(services =>
            {
                services.RunWolverineInSoloMode();
                services.DisableAllExternalWolverineTransports();
            });
        },
        securityStub,
        configOverride);
    }

    public async ValueTask DisposeAsync()
    {
        await this.Host.DisposeAsync();
        await this.db.DisposeAsync();
    }
}