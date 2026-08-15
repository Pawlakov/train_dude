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

using TrainDude.Commands.Requests.Admin;
using TrainDude.Domain.Documents;
using TrainDude.Shared.Values;

public sealed class UpdateStationNameModeCommandHandler
    : ICommandHandler<UpdateStationNameModeCommand>
{
    private readonly IDocumentSession session;

    public UpdateStationNameModeCommandHandler(IDocumentSession session)
    {
        this.session = session;
    }

    public async ValueTask<Unit> Handle(UpdateStationNameModeCommand command, CancellationToken cancellationToken)
    {
        var allSettingsIds = await this.session.Query<Settings>().Select(x => x.Id).ToListAsync(cancellationToken);
        if (allSettingsIds.Count < 1)
        {
            var newSettingsId = Guid.NewGuid();
            var created = Settings.Make(newSettingsId, StationNameMode.Modern);

            this.session.Events.StartStream<Settings>(newSettingsId, created);
            await this.session.SaveChangesAsync(cancellationToken);
        }
        else if (allSettingsIds.Count > 1)
        {
            // TODO fix this for good by ensuring only one command can be handled at a time.
            throw new ApplicationException("Too many settings were created");
        }

        var settingsId = await this.session.Query<Settings>().Select(x => x.Id).SingleAsync(cancellationToken);
        var stream = await this.session.Events.FetchForWriting<Settings>(settingsId, cancellationToken);

        var stationNameModeUpdated = stream.Aggregate.UpdateStationNameMode(command.Mode);

        stream.AppendOne(stationNameModeUpdated);
        await this.session.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}