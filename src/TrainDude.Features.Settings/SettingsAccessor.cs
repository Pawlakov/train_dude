// <copyright file="SettingsAccessor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings;

using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;

using Marten;

using TrainDude.Features.Settings.Domain;
using TrainDude.Features.Shared;
using TrainDude.Features.Shared.Extensions;

public static class SettingsAccessor
{
    private static readonly SemaphoreSlim SingletonLock = new(1, 1);

    public static Guid SingletonId { get; } = new("25003045-0000-0000-0000-000000000000");

    internal static async Task ExecuteWithSettings(IDocumentSession session, ClaimsPrincipal user, Func<IEventStream<SettingsAggregate>, SettingsAggregate, Task> action, CancellationToken cancellationToken = default)
    {
        await SingletonLock.WaitAsync(cancellationToken);

        try
        {
            var stream = await session.Events.FetchForWriting<SettingsAggregate>(SingletonId, cancellationToken);
            var aggregate = stream.Aggregate;
            if (aggregate is null)
            {
                var created = SettingsAggregate.Make(SingletonId, user.GetSubject());
                stream.AppendOne(created);

                aggregate = new SettingsAggregate();
                aggregate.Apply(created);
            }

            await action(stream, aggregate);
            await session.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            SingletonLock.Release();
        }
    }

    internal static async Task<SettingsAggregate> FetchForReading(IQuerySession session, ClaimsPrincipal user, CancellationToken cancellationToken = default)
    {
        var aggregate = await session.LoadAsync<SettingsAggregate>(SingletonId, cancellationToken);
        if (aggregate is null)
        {
            aggregate = new SettingsAggregate();
            aggregate.Apply(SettingsAggregate.Make(SingletonId, user.GetSubject()));
        }

        return aggregate;
    }
}