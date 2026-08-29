// <copyright file="BaseDomainEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Base;

using System;

public abstract record class BaseDomainEvent<TAggregate>(DateTime When) where TAggregate : BaseAggregate; // TODO base type should also tell who did it after we have auth