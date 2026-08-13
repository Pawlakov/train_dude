// <copyright file="SeedCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Admin;

using Mediator;

using TrainDude.Commands.Requests.Base;

/// <summary>
/// A command which seeds basic network data.
/// </summary>
public sealed record class SeedCommand
    : BasePolymorphicCommand, ICommand
{
}