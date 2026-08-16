// <copyright file="DropCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Admin;

using Mediator;

using TrainDude.Commands.Requests.Base;

public sealed record class DropCommand
    : BasePolymorphicCommand, ICommand
{
}