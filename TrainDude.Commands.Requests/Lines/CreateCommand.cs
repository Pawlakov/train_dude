// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Lines;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class CreateCommand
    : BaseRoutedCommand
{
    public const string Route = "line/create";
    
    public CreateCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public int Number { get; set; }

    public char? Letter { get; set; }
}