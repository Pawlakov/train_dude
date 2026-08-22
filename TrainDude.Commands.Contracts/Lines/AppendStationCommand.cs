// <copyright file="AppendStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Lines;

using System;

using TrainDude.Commands.Contracts.Base;

public sealed record class AppendStationCommand
    : BaseRoutedCommand, IVersionedDomainCommand
{
    public const string Route = "/line/station/assign";

    public AppendStationCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public long Version { get; set; }

    public Guid StationId { get; set; }
}