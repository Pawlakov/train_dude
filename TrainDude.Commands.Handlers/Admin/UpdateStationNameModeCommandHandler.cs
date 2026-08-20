// <copyright file="UpdateStationNameModeCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Handlers.Services;
using TrainDude.Commands.Requests.Admin;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Admin;

using Wolverine;

public static class UpdateStationNameModeCommandHandler
{
    public static async Task<OutgoingMessages> HandleAsync(UpdateStationNameModeCommand command, IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var (stream, aggregate) = await SettingsService.FetchForWriting(session, cancellationToken);

        var stationNameModeUpdated = aggregate.UpdateStationNameMode(command.Mode);
        stream.AppendOne(stationNameModeUpdated);

        Func<Station, string> nameSelector = SettingsService.BuildNameSelector(command.Mode);

        var allStations = await session.Query<Station>().ToListAsync(cancellationToken);
        var newNameDictionary = allStations.ToDictionary(x => x.Id, nameSelector);

        var integrationEvent = new SettingsStationNameModeUpdatedIntegrationEvent(command.Mode, newNameDictionary);

        return new OutgoingMessages { integrationEvent };
    }
}