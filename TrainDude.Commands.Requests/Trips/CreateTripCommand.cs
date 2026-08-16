// <copyright file="CreateTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Trips;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class CreateTripCommand
    : BasePolymorphicCommand
{
    public Guid Id { get; set; }

    public int Number { get; set; }
}