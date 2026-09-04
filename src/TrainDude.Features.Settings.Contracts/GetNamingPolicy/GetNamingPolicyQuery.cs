// <copyright file="GetNamingPolicyQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Contracts.GetNamingPolicy;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetNamingPolicyQuery()
    : IDomainQuery<GetNamingPolicyResult>
{
    public const string TypeRoute = "/settings/name-mode";

    public string Route => TypeRoute;
}