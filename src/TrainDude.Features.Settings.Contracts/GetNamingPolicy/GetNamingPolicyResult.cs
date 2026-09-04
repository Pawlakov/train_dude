// <copyright file="GetNamingPolicyResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.Contracts.GetNamingPolicy;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Shared.Enums;

public sealed record GetNamingPolicyResult(NamingPolicy Policy) : IQueryResult;