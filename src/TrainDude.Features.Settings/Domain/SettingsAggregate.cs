// <copyright file="SettingsAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Domain;

using System;
using System.Text.Json.Serialization;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared.Contracts.Enums;

public class SettingsAggregate
{
    [JsonConstructor]
    private SettingsAggregate(Guid id, long version, NamingPolicy namingPolicy)
    {
        this.Id = id;
        this.Version = version;

        this.NamingPolicy = namingPolicy;
    }

    public SettingsAggregate()
    {
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public NamingPolicy NamingPolicy { get; private set; }

    public static SettingsCreated Make(Guid id, string who)
    {
        return new SettingsCreated(id, who);
    }

    public SettingsNamingPolicySet SetNamingPolicy(string who, NamingPolicy namingPolicy)
    {
        return new SettingsNamingPolicySet(this.Id, who, namingPolicy);
    }

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