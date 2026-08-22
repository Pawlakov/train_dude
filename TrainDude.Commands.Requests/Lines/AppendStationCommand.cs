// <copyright file="AppendStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Lines;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class AppendStationCommand
    : BaseRoutedCommand
{
    public const string Route = "line/station/assign";

    public AppendStationCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public long Version { get; set; }

    public Guid StationId { get; set; }
}