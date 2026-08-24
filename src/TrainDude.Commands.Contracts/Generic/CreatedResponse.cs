// <copyright file="CreatedResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Generic;

using System;

using TrainDude.Commands.Contracts.Base;

public sealed record class CreatedResponse(Guid Id) : BaseCommandResponse();