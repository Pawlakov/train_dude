// <copyright file="SettingsCreatedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Settings;

using System;

using Mediator;

public sealed record class SettingsCreatedNotification(Guid SettingsId) : INotification;