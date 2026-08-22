// <copyright file="SettingsCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Admin;

using System;

using TrainDude.Domain.Events.Base;

public sealed record class SettingsCreated(Guid Id, DateTime When) : BaseAggregateEvent(Id, When);