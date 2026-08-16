// <copyright file="CreateTripCommandResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Trips;

using System;

using TrainDude.Commands.Requests.Base;

public class CreateTripCommandResult
    : BasePolymorphicCommandResponse
{
    required public Guid Id { get; set; }
}