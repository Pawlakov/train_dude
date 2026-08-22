// <copyright file="AssignTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Lines;

using System;

using TrainDude.Commands.Contracts.Base;

public sealed record class AssignTripCommand
    : BaseRoutedCommand, IVersionedDomainCommand
{
    public const string Route = "/line/trip/assign";

    public AssignTripCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public long Version { get; set; }

    public Guid TripId { get; set; }
}