// <copyright file="SharedSettingsReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.ReadModels;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Shared.Contracts.Enums;

public sealed class SharedSettingsReference
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public NamingPolicy NamingPolicy { get; set; }
}