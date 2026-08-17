// <copyright file="UpdateStationNameModeCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using System;
using System.Linq;

using Marten;

using TrainDude.Commands.Requests.Admin;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Admin;
using TrainDude.Integration.Values;

using Wolverine;

public static class UpdateStationNameModeCommandHandler
{
    public static OutgoingMessages Handle(UpdateStationNameModeCommand command, IDocumentSession session)
    {
        var allSettingsIds = session.Query<Settings>().Select(x => x.Id).ToList();
        if (allSettingsIds.Count < 1)
        {
            var newSettingsId = Guid.NewGuid();
            var created = Settings.Make(newSettingsId);

            session.Events.StartStream<Settings>(newSettingsId, created);
            session.SaveChangesAsync().Wait(); // This requires no integration. Read model doesn't need to know about this mess.
        }
        else if (allSettingsIds.Count > 1)
        {
            // TODO fix this for good by ensuring only one command can be handled at a time.
            throw new ApplicationException("Too many settings were created");
        }

        var settingsId = session.Query<Settings>().Select(x => x.Id).Single();
        var stream = session.Events.FetchForWriting<Settings>(settingsId).Result;

        var stationNameModeUpdated = stream.Aggregate.UpdateStationNameMode(command.Mode);

        stream.AppendOne(stationNameModeUpdated);

        var integrationEvent = new SettingsStationNameModeUpdatedIntegrationEvent(command.Mode);

        return new OutgoingMessages { integrationEvent };
    }
}