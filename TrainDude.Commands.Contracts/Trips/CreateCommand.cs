// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Trips;

using System;

using TrainDude.Commands.Contracts.Base;

public sealed record class CreateCommand
    : BaseRoutedCommand, IDomainCommand
{
    public const string Route = "/trip/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public int Number { get; set; }
}