// <copyright file="CreatedResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Generic;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreatedResponse(Guid Id) : IResponse;