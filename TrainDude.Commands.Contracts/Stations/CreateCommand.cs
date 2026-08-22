// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Stations;

using System;

using TrainDude.Commands.Contracts.Base;

public sealed record class CreateCommand
    : BaseRoutedCommand, IDomainCommand
{
    public const string Route = "/station/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public string NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public string? NamePolish { get; set; }

    public string? NameRussian { get; set; }
}