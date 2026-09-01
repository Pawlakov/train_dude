// <copyright file="LineSegmentAppended.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain.Events;

using System;

public sealed record LineSegmentAppended(Guid Id, DateTime When, Guid SegmentId);