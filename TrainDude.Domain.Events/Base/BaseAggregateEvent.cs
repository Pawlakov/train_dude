// <copyright file="BaseAggregateEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Base;

using System;

public abstract record class BaseAggregateEvent(Guid Id, DateTime When) : BaseDomainEvent(When);