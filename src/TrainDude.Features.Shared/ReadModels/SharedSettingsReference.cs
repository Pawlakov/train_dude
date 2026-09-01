// <copyright file="SettingsReferenceReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.ReadModels;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Shared.Enums;

public sealed class SharedSettingsReference
{
    [JsonConstructor]
    public SharedSettingsReference(Guid id, long version, NamingPolicy namingPolicy)
    {
        this.Id = id;
        this.Version = version;

        this.NamingPolicy = namingPolicy;
    }

    public SharedSettingsReference()
    {
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public NamingPolicy NamingPolicy { get; private set; }

    public void Apply(SettingsCreated e)
    {
        this.Id = e.Id;
        this.NamingPolicy = NamingPolicy.Modern;

        this.Version++;
    }

    public void Apply(SettingsNamingPolicySet e)
    {
        this.NamingPolicy = e.NamingPolicy;

        this.Version++;
    }
}