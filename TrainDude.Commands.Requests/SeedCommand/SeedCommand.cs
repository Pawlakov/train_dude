// <copyright file="DropAndSeedCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.DropAndSeedCommand;

using Mediator;

using TrainDude.Commands.Requests.Base;

/// <summary>
/// A command which seeds basic network data.
/// </summary>
public sealed record class SeedCommand
    : BasePolymorphicCommand, ICommand
{
}