// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Segments;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class CreateCommand
    : BaseCreateCommand
{
    public const string Route = "/segment/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public double? NominalLength { get; set; }

    public Guid AId { get; set; }

    public Guid BId { get; set; }
}