// <copyright file="LineSegmentAppendedWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Events;

using System;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.ReadModels.Values;

public sealed record LineSegmentAppendedWithReferences(LineSegmentAppended Event, LineReadModelSegment Segment);