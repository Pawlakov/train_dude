// <copyright file="UpdateStationNameModeCommandDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using System;
using System.Linq;
using System.Threading;

using FluentValidation;

using Marten;

using TrainDude.Commands.Data.Documents;
using TrainDude.Commands.Handlers.Validation;
using TrainDude.Commands.Requests.Admin;

public class UpdateStationNameModeCommandDataValidator
    : AbstractWriteDataValidator<UpdateStationNameModeCommand>
{
    public UpdateStationNameModeCommandDataValidator(IDocumentSession session)
    {
        this.RuleFor(command => command.SettingsId)
            .MustAsync(async (Guid? id, CancellationToken cancellationToken) => (id.HasValue && await session.Query<Settings>().AnyAsync(x => x.Id == id, cancellationToken)) || (!id.HasValue && !(await session.Query<Settings>().AnyAsync(cancellationToken))))
            .WithMessage("This is not the settings ID.");
    }
}