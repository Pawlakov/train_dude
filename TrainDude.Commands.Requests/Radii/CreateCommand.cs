// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Radii;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class CreateCommand
    : BaseRoutedCommand
{
    public const string Route = "radius/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public int Speed { get; set; }

    public int Minimum { get; set; }
}