// <copyright file="SegmentCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Segments;

using System;

using TrainDude.Domain.Events.Base;

public sealed record class SegmentCreated(Guid Id, DateTime When, double? NominalLength, Guid AId, Guid BId) : BaseAggregateEvent(Id, When);