// <copyright file="SettingsCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Domain.Events;

using System;

using TrainDude.Features.Shared.Base;

public sealed record SettingsCreated(Guid SettingsId, string Who)
    : IDomainEvent
{
    public Guid Id => this.SettingsId;
}