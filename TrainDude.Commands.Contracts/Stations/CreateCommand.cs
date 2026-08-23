// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Stations;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class CreateCommand
    : BaseCreateCommand
{
    public const string Route = "/station/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public string NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NameRussian { get; set; }
}