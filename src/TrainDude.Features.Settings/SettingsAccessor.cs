// <copyright file="SettingsAccessor.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings;

using System;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Settings.Domain;
using TrainDude.Features.Shared;
using TrainDude.Shared.Enums;

public static class SettingsAccessor
{
    private static readonly SemaphoreSlim SingletonLock = new(1, 1);

    public static async Task ExecuteWithSettings(IDocumentSession session, Func<SettingsDocument, Task> action, CancellationToken cancellationToken = default)
    {
        await SingletonLock.WaitAsync(cancellationToken);

        try
        {
            var aggregate = await session.LoadAsync<SettingsDocument>(SettingsSingleton.Id, cancellationToken);
            if (aggregate is null)
            {
                aggregate = new SettingsDocument(SettingsSingleton.Id, NamingPolicy.Modern);
                session.Insert(aggregate);
            }

            await action(aggregate);
            await session.SaveChangesAsync(cancellationToken);
        }
        finally
        {
            SingletonLock.Release();
        }
    }

    public static async Task<SettingsDocument> FetchForReading(IQuerySession session, CancellationToken cancellationToken = default)
    {
        var aggregate = await session.LoadAsync<SettingsDocument>(SettingsSingleton.Id, cancellationToken);
        if (aggregate is null)
        {
            aggregate = new SettingsDocument(SettingsSingleton.Id, NamingPolicy.Modern);
        }

        return aggregate;
    }
}