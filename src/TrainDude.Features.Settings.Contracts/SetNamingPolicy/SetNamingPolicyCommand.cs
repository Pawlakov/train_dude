// <copyright file="SetNamingPolicyCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Contracts.SetNamingPolicy;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Enums;

public sealed record SetNamingPolicyCommand(NamingPolicy Policy)
    : IEmptyCommand
{
    public const string TypeRoute = "/api/settings/name-mode";

    public string Route => TypeRoute;
}