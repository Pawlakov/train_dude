// <copyright file="ISpecificCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Shared.Contracts.Base;

using System;

public interface ISpecificCommand
    : ICommand
{
    Guid Id { get; }
}