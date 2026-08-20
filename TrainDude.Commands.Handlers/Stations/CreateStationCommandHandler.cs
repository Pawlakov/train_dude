// <copyright file="CreateStationCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Stations;

using System;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Handlers.Services;
using TrainDude.Commands.Requests.Stations;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Stations;
using TrainDude.Integration.Values;

using Wolverine;
using Wolverine.Marten;

public static class CreateStationCommandHandler
{
    public static void Validate(CreateStationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.NameGerman))
        {
            throw new InvalidOperationException("A valid name is required.");
        }
    }

    public static async Task<(IStartStream, OutgoingMessages)> HandleAsync(CreateStationCommand command, SettingsService settingsService, CancellationToken cancellationToken = default)
    {
        var domainEvent = Station.Make(command.Id, command.NameGerman, command.NameGermanNew, command.NamePolish, command.NameRussian);

        var startStream = MartenOps.StartStream<Station>(domainEvent.Id, domainEvent);

        var nameSelector = await settingsService.GetNameSelector(cancellationToken);
        var stationName = nameSelector(domainEvent);

        var integrationEvent = new StationCreatedIntegrationEvent(domainEvent.Id, 1L, stationName);

        return (startStream, new OutgoingMessages { integrationEvent });
    }
}