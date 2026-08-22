// <copyright file="SetNameModeEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Settings;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Contracts.Admin;
using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Settings;
using TrainDude.Domain;
using TrainDude.Domain.Stations;
using TrainDude.Integration.Events.Settings;

using Wolverine;
using Wolverine.Http;

public static class SetNameModeEndpoint
{
    [WolverinePost(SetNameModeCommand.Route)]
    public static async Task<(EmptyResponse, OutgoingMessages)> Post(SetNameModeCommand command, IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var (stream, aggregate) = await SettingsAccessor.FetchForWriting(session, cancellationToken);

        var stationNameModeUpdated = aggregate.UpdateStationNameMode(command.Mode);
        stream.AppendOne(stationNameModeUpdated);

        Func<StationAggregate, string> nameSelector = StationNameResolver.BuildNameSelector(command.Mode);

        var allStations = await session.Query<StationAggregate>().ToListAsync(cancellationToken);
        var newNameDictionary = allStations.ToDictionary(x => x.Id, nameSelector);

        var result = new EmptyResponse();
        var integrationEvent = new SettingsStationNameModeUpdatedIntegrationEvent(command.Mode, newNameDictionary);

        return (result, new OutgoingMessages { integrationEvent });
    }
}