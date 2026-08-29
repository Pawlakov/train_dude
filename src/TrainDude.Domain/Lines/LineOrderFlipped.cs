// <copyright file="LineOrderFlipped.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;

using TrainDude.Domain.Base;
using TrainDude.Domain.Stations;

public sealed record class LineOrderFlipped(Guid Id, DateTime When) : BaseAggregateEvent<LineAggregate>(Id, When);