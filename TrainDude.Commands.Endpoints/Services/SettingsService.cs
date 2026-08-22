// <copyright file="SettingsService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;

using Marten;

using TrainDude.Domain.Documents;
using TrainDude.Domain.Events.Base;
using TrainDude.Integration.Values;

public class SettingsService
{
    public static readonly Guid SingletonId = Guid.Parse("25003045-0000-0000-0000-000000000000");

    private const string FallbackStationName = "???";

    private readonly IDocumentSession session;

    public SettingsService(IDocumentSession session)
    {
        this.session = session;
    }

    public static async Task<(IEventStream<Settings> Stream, Settings Aggregate)> FetchForWriting(IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var stream = await session.Events.FetchForWriting<Settings>(SingletonId, cancellationToken);

        var aggregate = stream.Aggregate;
        if (aggregate is null)
        {
            var created = Settings.Make(SingletonId);
            stream.AppendOne(created);

            aggregate = new Settings();
            aggregate.Apply(created);
        }

        return (stream, aggregate);
    }

    public static Func<IHasAlternativeNames, string> BuildNameSelector(StationNameMode mode) =>
        station => SelectName(mode, station.NameGerman, station.NameGermanNew, station.NamePolish, station.NameRussian);

    public async Task<Func<IHasAlternativeNames, string>> GetNameSelector(CancellationToken cancellationToken = default)
    {
        var mode = await this.GetStationNameMode(cancellationToken);
        return BuildNameSelector(mode);
    }

    private async Task<StationNameMode> GetStationNameMode(CancellationToken cancellationToken = default)
    {
        var settings = await this.session.Query<Settings>().SingleOrDefaultAsync(cancellationToken);
        return settings?.StationNameMode ?? StationNameMode.Modern;
    }

    private static string SelectName(StationNameMode mode, string german, string? germanNew, string? polish, string? russian) =>
        mode switch
        {
            StationNameMode.German => germanNew ?? german,
            _ => polish ?? russian ?? FallbackStationName,
        };
}