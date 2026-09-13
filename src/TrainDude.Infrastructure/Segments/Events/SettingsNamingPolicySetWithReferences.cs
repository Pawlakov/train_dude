// <copyright file="SettingsNamingPolicySetWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Segments.Events;

using TrainDude.Features.Settings.Domain.Events;

public sealed record SettingsNamingPolicySetWithReferences(SettingsNamingPolicySet Event, string AName, string BName);