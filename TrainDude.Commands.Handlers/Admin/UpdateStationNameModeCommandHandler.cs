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
        var allSettings = await this.session.Query<Settings>().ToListAsync(cancellationToken);
        if (allSettings.Count == 0 && command.SettingsId == null)
        {
            var settings = Settings.Create(Guid.NewGuid(), StationNameMode.Modern);

            settings.UpdateStationNameMode(command.Mode);

            this.session.Store(settings);
        }
        else if (allSettings.Count > 0 && command.SettingsId.HasValue)
        {
            var settings = allSettings.Single(x => x.Id == command.SettingsId.Value);

            settings.UpdateStationNameMode(command.Mode);

            this.session.Store(settings);

            if (allSettings.Count > 1)
            {
                this.session.Delete(allSettings.Skip(1));
            }
        }
        else
        {
            throw new ApplicationException("If this exception was thrown it means that the validation has failed.");
        }

        await this.session.SaveChangesAsync(cancellationToken);

        await this.publisher.Publish(new DataChangedNotification(), cancellationToken);

        return Unit.Value;
    }
}