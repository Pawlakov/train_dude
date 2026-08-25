// <copyright file="SettingsService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints;

using System;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;

using Marten;

using TrainDude.Domain.Settings;
using TrainDude.Shared.Enums;
using TrainDude.Shared.Values;

public static class SettingsAccessor
{
    // IMPORTANT:
    // This semaphore only guards the singleton within a single process.
    private static readonly SemaphoreSlim SingletonLock = new(1, 1);

    public static Guid SingletonId { get; } = new("25003045-0000-0000-0000-000000000000");

    public static async Task ExecuteWithSettings(
        IDocumentSession session,
        Func<IEventStream<SettingsDocument>, SettingsDocument, Task> action,
        CancellationToken cancellationToken = default)
    {
        await SingletonLock.WaitAsync(cancellationToken);

        try
        {
            var (stream, aggregate) = await LoadForWriting(session, cancellationToken);

            await action(stream, aggregate);
            await session.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            SingletonLock.Release();
        }
    }

    public static async Task<SettingsDocument> FetchForReading(IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var aggregate = await session.Events.AggregateStreamAsync<SettingsDocument>(SingletonId, token: cancellationToken);

        if (aggregate is null)
        {
            aggregate = new SettingsDocument();
            aggregate.Apply(SettingsDocument.Make(SingletonId));
        }

        return aggregate;
    }

    public static async Task<StationNameMode> GetNameMode(IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var aggregate = await FetchForReading(session, cancellationToken);
        return aggregate.StationNameMode;
    }

    private static async Task<(IEventStream<SettingsDocument> Stream, SettingsDocument Aggregate)> LoadForWriting(
        IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var stream = await session.Events.FetchForWriting<SettingsDocument>(SingletonId, cancellationToken);

        var aggregate = stream.Aggregate;

        if (aggregate is null)
        {
            var created = SettingsDocument.Make(SingletonId);
            stream.AppendOne(created);

            aggregate = new SettingsDocument();
            aggregate.Apply(created);
        }

        return (stream, aggregate);
    }
}