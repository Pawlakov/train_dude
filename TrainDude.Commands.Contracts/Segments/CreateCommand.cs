// <copyright file="CreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Segments;

using System;

using TrainDude.Commands.Contracts.Base;

public sealed record class CreateCommand
    : BaseRoutedCommand, IDomainCommand
{
    public const string Route = "/segment/create";

    public CreateCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public double? NominalLength { get; set; }

    public Guid AId { get; set; }

    public Guid BId { get; set; }
}