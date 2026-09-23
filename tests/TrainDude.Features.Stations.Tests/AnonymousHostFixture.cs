// <copyright file="AnonymousHostFixture.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Alba;

using JasperFx.CommandLine;

using TrainDude.Web;

using Wolverine;

public class AnonymousHostFixture
    : BaseHostFixture
{
    public override async Task InitializeAsync()
    {
        var configValues = new Dictionary<string, string?> { ["ConnectionStrings:Write"] = this.Postgres.Container.GetConnectionString() };
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
}