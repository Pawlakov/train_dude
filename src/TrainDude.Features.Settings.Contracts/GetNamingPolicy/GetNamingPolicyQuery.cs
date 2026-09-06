// <copyright file="GetNamingPolicyQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Contracts.GetNamingPolicy;

using System.Text.Json.Serialization;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetNamingPolicyQuery()
    : IDomainQuery<GetNamingPolicyResult>
{
    public const string TypeRoute = "/api/settings/name-mode";

    [JsonIgnore]
    public string Route => TypeRoute;
}