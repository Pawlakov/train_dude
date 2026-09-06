// <copyright file="AppendSegmentCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.AppendSegment;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record AppendSegmentCommand(Guid LineId, long Version, Guid SegmentId)
    : IUpdateCommand
{
    public const string TypeRoute = "/api/line/segments/append";

    public string Route => TypeRoute;

    public Guid Id => this.LineId;
}