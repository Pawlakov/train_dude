// <copyright file="CreatedResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Generic;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreatedResult(Guid Id) : ICommandResult;