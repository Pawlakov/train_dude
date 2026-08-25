// <copyright file="SetCourseCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Segments;

using System.Collections.Generic;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Shared;
using TrainDude.Shared.Values;

public sealed record class SetCourseCommand
    : BaseUpdateCommand
{
    public const string Route = "/segment/course/set";

    public SetCourseCommand()
        : base(Route)
    {
    }

    public IEnumerable<Location> Course { get; set; }
}