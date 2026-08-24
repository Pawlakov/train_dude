// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Radii;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class CreateCommand
    : BaseCreateCommand
{
    public const string Route = "/radius/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public int Speed { get; set; }

    public int Minimum { get; set; }
}