// <copyright file="CreatedResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Generic;

using System;

public sealed record class CreatedResponse(Guid Id);