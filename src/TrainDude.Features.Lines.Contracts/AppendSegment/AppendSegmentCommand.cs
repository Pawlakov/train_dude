// <copyright file="AppendSegmentCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.AppendSegment;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record AppendSegmentCommand(Guid SegmentId)
    : ISpecificCommand
{
    public const string TypeRoute = "/api/lines/{0}/append-segment";

    public static string Route => TypeRoute;
}