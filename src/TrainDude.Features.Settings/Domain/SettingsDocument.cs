// <copyright file="SettingsDocument.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Domain;

using System;
using System.Text.Json.Serialization;

using TrainDude.Shared.Enums;

public class SettingsDocument
{
    [JsonConstructor]
    internal SettingsDocument(Guid id, NamingPolicy namingPolicy)
    {
        this.Id = id;

        this.NamingPolicy = namingPolicy;
    }

    public Guid Id { get; private set; }

    public NamingPolicy NamingPolicy { get; set; }
}