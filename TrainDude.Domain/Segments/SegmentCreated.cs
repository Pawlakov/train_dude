// <copyright file="SegmentCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Segments;

using System;

using TrainDude.Domain.Base;

public sealed record class SegmentCreated(Guid Id, DateTime When, double? NominalLength, Guid AId, Guid BId) : BaseAggregateEvent(Id, When);