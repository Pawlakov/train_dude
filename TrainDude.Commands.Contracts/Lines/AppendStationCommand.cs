// <copyright file="AppendStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Lines;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class AppendStationCommand
    : BaseUpdateCommand
{
    public const string Route = "/line/station/append";

    public AppendStationCommand()
        : base(Route)
    {
    }

    public Guid StationId { get; set; }
}