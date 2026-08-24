// <copyright file="LineStationAppended.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;

using TrainDude.Domain.Base;

public sealed record class LineStationAppended(Guid Id, DateTime When, Guid StationId) : BaseAggregateEvent(Id, When);