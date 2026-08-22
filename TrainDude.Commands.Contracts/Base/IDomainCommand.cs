// <copyright file="IDomainCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Contracts.Base;

using System;

public interface IDomainCommand
{
    Guid Id { get; }
}