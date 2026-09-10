// <copyright file="SegmentCourseSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Domain.Events;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Values;

public sealed record SegmentCourseSet(Guid Id, string Who, IEnumerable<Location> Course);