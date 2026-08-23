// <copyright file="AddAxleCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Stations;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class AddAxleCommand
    : BaseUpdateCommand
{
    public const string Route = "/station/axle/add";

    public AddAxleCommand()
        : base(Route)
    {
    }
}