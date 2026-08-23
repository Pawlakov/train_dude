// <copyright file="SetNameModeCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Settings;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;
using TrainDude.Shared.Values;

public sealed record class SetNameModeCommand
    : BaseRoutedCommand<EmptyResponse>
{
    public const string Route = "/admin/name-mode/set";

    public SetNameModeCommand()
        : base(Route)
    {
    }

    public required StationNameMode Mode { get; set; }
}