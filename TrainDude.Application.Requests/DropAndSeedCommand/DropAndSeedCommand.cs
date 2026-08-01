// <copyright file="DropAndSeedCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.DropAndSeedCommand;

using Mediator;

using TrainDude.Application.Requests.Base;

/// <summary>
/// A command which seeds basic network data.
/// </summary>
public sealed record class DropAndSeedCommand
    : BasePolymorphicCommand, ICommand
{
}