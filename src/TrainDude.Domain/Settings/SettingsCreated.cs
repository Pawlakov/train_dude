// <copyright file="SettingsCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Settings;

using System;

using TrainDude.Domain.Base;

public sealed record class SettingsCreated(Guid Id, DateTime When) : BaseAggregateEvent(Id, When);