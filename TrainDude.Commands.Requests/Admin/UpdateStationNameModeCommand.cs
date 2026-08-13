// <copyright file="UpdateStationNameModeCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Admin;

using System;

using Mediator;

using TrainDude.Commands.Requests.Base;
using TrainDude.Shared.Values;

public sealed record class UpdateStationNameModeCommand
    : BasePolymorphicCommand, ICommand
{
    required public Guid? SettingsId { get; set; }

    required public StationNameMode Mode { get; set; }
}