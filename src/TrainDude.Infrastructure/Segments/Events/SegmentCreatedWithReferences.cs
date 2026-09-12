// <copyright file="SegmentCreatedWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Segments.Events;

using System;

using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;

public sealed record SegmentCreatedWithReferences(SegmentCreated Event, SegmentEndReference A, SegmentEndReference B);