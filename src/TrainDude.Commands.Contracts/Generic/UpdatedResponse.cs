// <copyright file="UpdatedResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Generic;

using TrainDude.Commands.Contracts.Base;

public sealed record class UpdatedResponse(long Version) : BaseCommandResponse();