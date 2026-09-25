// <copyright file="BaseEndpointTests.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;

using Alba;

using Marten;

using TrainDude.Features.Shared.Contracts.Base;

using TUnit.Core.Services;

public abstract class BaseEndpointTests
{
    [ClassDataSource<AuthenticatedHostFixture>(Shared = SharedType.PerTestSession)]
    public required AuthenticatedHostFixture AuthFixture { get; init; }

    [ClassDataSource<AnonymousHostFixture>(Shared = SharedType.PerTestSession)]
    public required AnonymousHostFixture AnonFixture { get; init; }

    protected async Task<IScenarioResult> PostCreateCommandAsync<TRequest>(TRequest command, int expectedStatus = 201, bool authenticated = true)
        where TRequest : ICreateCommand
    {
        BaseHostFixture fixture = authenticated ? this.AuthFixture : this.AnonFixture;
        return await fixture.Host.Scenario(x =>
        {
            x.Post.Json(command).ToUrl(TRequest.Route);
            x.StatusCodeShouldBe(expectedStatus);
        });
    }

    protected async Task<IScenarioResult> PostUpdateCommandAsync<TRequest>(TRequest command, int expectedStatus = 200, bool authenticated = true)
        where TRequest : ISpecificCommand
    {
        var route = TRequest.Route.Replace("{id}", command.Id.ToString());
        BaseHostFixture fixture = authenticated ? this.AuthFixture : this.AnonFixture;
        return await fixture.Host.Scenario(x =>
        {
            x.Post.Json(command).ToUrl(route);
            x.StatusCodeShouldBe(expectedStatus);
        });
    }

    protected async Task AssertNothingWrittenAsync(long? baseEventSequence = null, bool authenticated = true)
    {
        BaseHostFixture fixture = authenticated ? this.AuthFixture : this.AnonFixture;
        var store = fixture.Host.Services.GetRequiredService<IDocumentStore>();
        await using var session = store.QuerySession();
        if (baseEventSequence.HasValue)
        {
            var events = await session.Events.QueryAllRawEvents().Where(x => x.Sequence > baseEventSequence.Value).ToListAsync();
            await Assert.That(events).IsEmpty();
        }
        else
        {
            var events = await session.Events.QueryAllRawEvents().ToListAsync();
            await Assert.That(events).IsEmpty();
        }
    }

    protected async Task<long?> GetCurrentEventSequenceAsync(bool authenticated = true)
    {
        BaseHostFixture fixture = authenticated ? this.AuthFixture : this.AnonFixture;
        var store = fixture.Host.Services.GetRequiredService<IDocumentStore>();
        await using var session = store.QuerySession();
        var events = await session.Events.QueryAllRawEvents().ToListAsync();
        return events.Any() ? events.Max(x => x.Sequence) : null;
    }
}