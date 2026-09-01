// <copyright file="UpdatedResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Generic;

using TrainDude.Features.Base;

public sealed record UpdatedResponse(long Version) : BaseCommandResponse();