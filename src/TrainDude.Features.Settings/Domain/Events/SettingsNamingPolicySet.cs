// <copyright file="SettingsNamingPolicySet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Domain.Events;

using System;

using TrainDude.Features.Shared.Contracts.Enums;

public sealed record SettingsNamingPolicySet(Guid Id, DateTime When, NamingPolicy NamingPolicy);