// <copyright file="BaseDomainEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Base;

using System;

public abstract record class BaseDomainEvent(DateTime When); // TODO typ bazowy zrobić na kto dokonał (po autentykacji)