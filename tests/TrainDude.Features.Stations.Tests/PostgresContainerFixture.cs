// <copyright file="PostgresContainerFixture.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests;

using System;
using System.Threading.Tasks;

using Testcontainers.PostgreSql;

using TUnit.Core.Interfaces;

public class PostgresContainerFixture
    : IAsyncInitializer, IAsyncDisposable
{
    public PostgreSqlContainer Container { get; } = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .Build();

    public Task InitializeAsync() => this.Container.StartAsync();

    public ValueTask DisposeAsync() => this.Container.DisposeAsync();
}