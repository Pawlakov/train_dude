// <copyright file="AddAxleCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Stations;

using System;

using TrainDude.Commands.Contracts.Base;

public sealed record class AddAxleCommand
    : BaseRoutedCommand, IVersionedDomainCommand
{
    public const string Route = "/station/axle/add";

    public AddAxleCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public long Version { get; set; }
}