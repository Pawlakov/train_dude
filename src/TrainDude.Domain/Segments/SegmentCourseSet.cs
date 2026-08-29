// <copyright file="SegmentCourseSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Domain.Segments;

using System;
using System.Collections.Generic;

using TrainDude.Domain.Base;
using TrainDude.Shared;
using TrainDude.Shared.Values;

public sealed record class SegmentCourseSet(Guid Id, DateTime When, IEnumerable<Location> Course) : BaseAggregateEvent<SegmentAggregate>(Id, When);