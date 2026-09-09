// <copyright file="LineSegmentAppendedWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Events;

using System;

using TrainDude.Features.Lines.Domain.Values;

public sealed record LineSegmentAppendedWithReferences(Guid Id, DateTime When, LineSegment Segment);