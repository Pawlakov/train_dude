// <copyright file="UpdatedResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record UpdatedResult(long Version) : ICommandResult;