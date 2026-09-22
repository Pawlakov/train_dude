// <copyright file="AnonymousHostFixture.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Tests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Alba;

using JasperFx.CommandLine;

using Testcontainers.PostgreSql;

using TrainDude.Web;

using TUnit.Core.Interfaces;

using Wolverine;

public class AnonymousHostFixture
    : IAsyncInitializer, IAsyncDisposable
{
    private readonly PostgreSqlContainer db = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .Build();

    public IAlbaHost Host { get; private set; }

    public async Task InitializeAsync()
    {
        await this.db.StartAsync();

        var configValues = new Dictionary<string, string?> { ["ConnectionStrings:Write"] = this.db.GetConnectionString() };
        var configOverride = ConfigurationOverride.Create(configValues);

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
        configOverride);
    }

    public async ValueTask DisposeAsync()
    {
        await this.Host.DisposeAsync();
        await this.db.DisposeAsync();
    }
}