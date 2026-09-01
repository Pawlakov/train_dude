// <copyright file="CreatedResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Generic;

using System;

using TrainDude.Features.Base;

public sealed record CreatedResponse(Guid Id) : BaseCommandResponse();