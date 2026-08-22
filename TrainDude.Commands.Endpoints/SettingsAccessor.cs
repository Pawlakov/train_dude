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
using TrainDude.Shared.Values;

public static class SettingsAccessor
{
    public static readonly Guid SingletonId = Guid.Parse("25003045-0000-0000-0000-000000000000");

    public static async Task<(IEventStream<SettingsDocument> Stream, SettingsDocument Aggregate)> FetchForWriting(IDocumentSession session, CancellationToken cancellationToken = default)
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

    public static async Task<StationNameMode> GetNameMode(IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var (_, aggregate) = await FetchForWriting(session, cancellationToken);
        return aggregate.StationNameMode;
    }
}