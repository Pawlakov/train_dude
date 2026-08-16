// <copyright file="CreateTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Trips;

using Mediator;

using TrainDude.Commands.Requests.Base;

public sealed record class CreateTripCommand
    : BasePolymorphicCommand, ICommand<CreateTripCommandResult>
{
    public int Number { get; set; }
}