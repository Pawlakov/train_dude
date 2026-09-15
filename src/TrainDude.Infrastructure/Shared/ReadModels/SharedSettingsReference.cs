// <copyright file="SharedSettingsReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Shared.ReadModels;

using System;

using TrainDude.Features.Shared.Contracts.Enums;

public sealed class SharedSettingsReference
{
    public Guid Id { get; set; }

    public NamingPolicy NamingPolicy { get; set; }
}