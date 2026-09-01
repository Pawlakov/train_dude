// <copyright file="SettingsCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Domain.Events;

using System;

public sealed record SettingsCreated(Guid Id, DateTime When);