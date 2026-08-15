// <copyright file="UpdateStationNameModeCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Mediator;

using TrainDude.Commands.Data.Documents;
using TrainDude.Commands.Requests.Admin;
using TrainDude.Shared.Notifications;
using TrainDude.Shared.Values;

public sealed class UpdateStationNameModeCommandHandler
    : ICommandHandler<UpdateStationNameModeCommand>
{
    private readonly IDocumentSession session;
    private readonly IPublisher publisher;

    public UpdateStationNameModeCommandHandler(IDocumentSession session, IPublisher publisher)
    {
        this.session = session;
        this.publisher = publisher;
    }

    public async ValueTask<Unit> Handle(UpdateStationNameModeCommand command, CancellationToken cancellationToken)
    {
        var settingsId = command.SettingsId ?? Guid.NewGuid();

        var allSettings = await this.session.Query<Settings>().ToListAsync(cancellationToken);
        var settings = (allSettings.Count, command.SettingsId) switch
        {
            (0, null) => Settings.Create(settingsId, StationNameMode.Modern),
            (> 0, not null) => allSettings.Single(x => x.Id == command.SettingsId.Value),
            _ => throw new ApplicationException("If this exception was thrown it means that the validation has failed."),
        };

        foreach (var fakeSettings in allSettings.Where(x => x.Id != settingsId))
        {
            this.session.Delete(fakeSettings);
        }

        settings.UpdateStationNameMode(command.Mode);

        // TODO nie zawsze start stream, co jak już jest
        this.session.Events.StartStream<Settings>(settingsId, settings.UncommittedEvents);
        await this.session.SaveChangesAsync(cancellationToken);
        foreach (var notification in settings.UncommittedEvents)
        {
            await this.publisher.Publish(notification, cancellationToken);
        }

        settings.ClearUncommittedEvents();

        return Unit.Value;
    }
}