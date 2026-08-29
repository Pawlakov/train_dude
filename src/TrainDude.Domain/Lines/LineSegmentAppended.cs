// <copyright file="LineSegmentAppended.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;

using TrainDude.Domain.Base;
using TrainDude.Domain.Segments;

public sealed record class LineSegmentAppended(Guid Id, DateTime When, Guid SegmentId) : BaseAggregateEvent<LineAggregate>(Id, When);