// <copyright file="AppendStationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Lines;

using System;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class AppendSegmentCommand
    : BaseUpdateCommand
{
    public const string Route = "/line/segment/append";

    public AppendSegmentCommand()
        : base(Route)
    {
    }

    public Guid SegmentId { get; set; }
}
