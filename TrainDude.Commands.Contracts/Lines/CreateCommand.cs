// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Lines;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class CreateCommand
    : BaseCreateCommand
{
    public const string Route = "/line/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public int Number { get; set; }

    public char? Letter { get; set; }
}