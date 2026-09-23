// <copyright file="BaseHostFixture.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests;

using System;
using System.Threading.Tasks;

using Alba;

using TUnit.Core.Interfaces;

public abstract class BaseHostFixture
    : IAsyncInitializer, IAsyncDisposable
{
    [ClassDataSource<PostgresContainerFixture>(Shared = SharedType.PerTestSession)]
    public required PostgresContainerFixture Postgres { get; init; }

    public IAlbaHost Host { get; protected set; }

    public async ValueTask DisposeAsync()
    {
        await this.Host.DisposeAsync();
    }

    public abstract Task InitializeAsync();
}