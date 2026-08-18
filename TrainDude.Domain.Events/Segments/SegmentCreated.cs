// <copyright file="SegmentCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Segments;

using System;

public sealed record class SegmentCreated(Guid Id, double? NominalLength, Guid AId, Guid BId) : IDomainEvent;