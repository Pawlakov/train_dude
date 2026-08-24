// <copyright file="SetLocationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Stations;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;
using TrainDude.Shared.Values;

public sealed record class SetLocationCommand
    : BaseUpdateCommand
{
    public const string Route = "/station/location/set";

    public SetLocationCommand()
        : base(Route)
    {
    }

    public Location Location { get; set; }
}