// <copyright file="LineStationAppended.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Lines;

using System;

public sealed record class LineStationAppended(Guid Id, Guid StationId) : IDomainEvent;