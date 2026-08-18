// <copyright file="AppendStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Lines;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class AppendStationCommand
    : BasePolymorphicCommand
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public Guid StationId { get; set; }
}