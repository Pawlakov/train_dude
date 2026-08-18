// <copyright file="CreateLineCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Lines;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class CreateLineCommand
    : BasePolymorphicCommand
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public char? Letter { get; set; }
}