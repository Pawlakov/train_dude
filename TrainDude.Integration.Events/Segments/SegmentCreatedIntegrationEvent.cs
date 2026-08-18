// <copyright file="SegmentCreatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Segments;

using System;

public sealed record class SegmentCreatedIntegrationEvent(Guid Id, long Version);