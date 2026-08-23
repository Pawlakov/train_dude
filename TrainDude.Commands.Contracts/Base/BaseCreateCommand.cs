// <copyright file="BaseCreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Base;

using System;

using TrainDude.Commands.Contracts.Generic;

public abstract record class BaseCreateCommand
    : BaseRoutedCommand<CreatedResponse>, IDomainCommand
{
    protected BaseCreateCommand(string route)
        : base(route)
    {
    }

    public Guid Id { get; set; }
}