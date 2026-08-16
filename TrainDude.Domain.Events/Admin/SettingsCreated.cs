// <copyright file="SettingsCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Admin;

using System;

public sealed record class SettingsCreated(Guid Id) : IDomainEvent;