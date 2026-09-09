// <copyright file="SettingsAccessor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings;

using System;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;

using Marten;

using TrainDude.Features.Settings.Domain;
using TrainDude.Features.Shared;

public static class SettingsAccessor
{
    private static readonly SemaphoreSlim SingletonLock = new(1, 1);

    public static async Task ExecuteWithSettings(IDocumentSession session, Func<IEventStream<SettingsAggregate>, SettingsAggregate, Task> action, CancellationToken cancellationToken = default)
    {
        await SingletonLock.WaitAsync(cancellationToken);

        try
        {
            var stream = await session.Events.FetchForWriting<SettingsAggregate>(SettingsSingleton.Id, cancellationToken);
            var aggregate = stream.Aggregate;
            if (aggregate is null)
            {
                var created = SettingsAggregate.Make(SettingsSingleton.Id);
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

    public static async Task<SettingsAggregate> FetchForReading(IQuerySession session, CancellationToken cancellationToken = default)
    {
        var aggregate = await session.LoadAsync<SettingsAggregate>(SettingsSingleton.Id, cancellationToken);
        if (aggregate is null)
        {
            aggregate = new SettingsAggregate();
            aggregate.Apply(SettingsAggregate.Make(SettingsSingleton.Id));
        }

        return aggregate;
    }
}