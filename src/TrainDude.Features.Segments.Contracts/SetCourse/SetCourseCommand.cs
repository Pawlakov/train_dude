// <copyright file="SetCourseCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.SetCourse;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Values;

public sealed record SetCourseCommand(IReadOnlyList<Location> Course)
    : ISpecificCommand
{
    public const string TypeRoute = "/api/segments/{0}/set-course";

    public static string Route => TypeRoute;
}