// <copyright file="SetCourseCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.SetCourse;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Shared.Values;

public sealed record SetCourseCommand(Guid SegmentId, long Version, IReadOnlyList<Location> Course)
    : IUpdateCommand
{
    public const string TypeRoute = "/api/segment/course/set";

    public string Route => TypeRoute;

    public Guid Id => this.SegmentId;
}