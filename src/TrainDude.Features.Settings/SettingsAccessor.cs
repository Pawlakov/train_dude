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

    public static async Task ExecuteWithSettings(IDocumentSession session, ClaimsPrincipal user, Func<IEventStream<SettingsAggregate>, SettingsAggregate, Task> action, CancellationToken cancellationToken = default)
    {
        await SingletonLock.WaitAsync(cancellationToken);

        try
        {
            var stream = await session.Events.FetchForWriting<SettingsAggregate>(SettingsSingleton.Id, cancellationToken);
            var aggregate = stream.Aggregate;
            if (aggregate is null)
            {
                var created = SettingsAggregate.Make(SettingsSingleton.Id, user.GetSubject());
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

    public static async Task<SettingsAggregate> FetchForReading(IQuerySession session, ClaimsPrincipal user, CancellationToken cancellationToken = default)
    {
        var aggregate = await session.LoadAsync<SettingsAggregate>(SettingsSingleton.Id, cancellationToken);
        if (aggregate is null)
        {
            aggregate = new SettingsAggregate();
            aggregate.Apply(SettingsAggregate.Make(SettingsSingleton.Id, user.GetSubject()));
        }

        return aggregate;
    }
}