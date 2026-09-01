// <copyright file="SetCourseCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.SetCourse;

using System;
using System.Collections.Generic;

using TrainDude.Features.Base;
using TrainDude.Shared.Values;

public sealed record SetCourseCommand(Guid SegmentId, long Version, IEnumerable<Location> Course) : BaseUpdateCommand(SegmentId, Version);