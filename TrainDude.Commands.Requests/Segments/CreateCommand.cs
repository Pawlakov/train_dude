// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Segments;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class CreateCommand
    : BaseRoutedCommand
{
    public const string Route = "segment/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public double? NominalLength { get; set; }

    public Guid AId { get; set; }

    public Guid BId { get; set; }
}