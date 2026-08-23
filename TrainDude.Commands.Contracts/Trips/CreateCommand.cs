// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Trips;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class CreateCommand
    : BaseCreateCommand
{
    public const string Route = "/trip/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public int Number { get; set; }
}