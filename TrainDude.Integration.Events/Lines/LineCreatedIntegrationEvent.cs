// <copyright file="LineCreatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Lines;

using System;

public sealed record class LineCreatedIntegrationEvent(Guid Id, long Version, int Number, char? Letter) : IVersionedEvent;