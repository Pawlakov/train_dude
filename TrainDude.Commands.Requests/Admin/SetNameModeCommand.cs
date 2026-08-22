// <copyright file="SetNameModeCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Admin;

using TrainDude.Commands.Requests.Base;
using TrainDude.Integration.Values;

public sealed record class SetNameModeCommand
    : BaseRoutedCommand
{
    public const string Route = "admin/name-mode/set";

    public SetNameModeCommand()
        : base(Route)
    {
    }

    required public StationNameMode Mode { get; set; }
}