// <copyright file="SetNameModeEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Admin;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Endpoints.Services;
using TrainDude.Commands.Requests.Admin;
using TrainDude.Commands.Requests.Generic;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Admin;

using Wolverine;
using Wolverine.Http;

public static class SetNameModeEndpoint
{
    [WolverinePost(SetNameModeCommand.Route)]
    public static async Task<(EmptyResponse, OutgoingMessages)> Post(SetNameModeCommand command, IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var (stream, aggregate) = await SettingsService.FetchForWriting(session, cancellationToken);

        var stationNameModeUpdated = aggregate.UpdateStationNameMode(command.Mode);
        stream.AppendOne(stationNameModeUpdated);

        Func<Station, string> nameSelector = SettingsService.BuildNameSelector(command.Mode);

        var allStations = await session.Query<Station>().ToListAsync(cancellationToken);
        var newNameDictionary = allStations.ToDictionary(x => x.Id, nameSelector);

        var result = new EmptyResponse();
        var integrationEvent = new SettingsStationNameModeUpdatedIntegrationEvent(command.Mode, newNameDictionary);

        return (result, new OutgoingMessages { integrationEvent });
    }
}