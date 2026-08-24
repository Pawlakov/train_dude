// <copyright file="BaseUpdateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Base;

using System;

using TrainDude.Commands.Contracts.Generic;

public abstract record class BaseUpdateCommand
    : BaseRoutedCommand<UpdatedResponse>, IVersionedDomainCommand
{
    protected BaseUpdateCommand(string route)
        : base(route)
    {
    }

    public Guid Id { get; set; }

    public long Version { get; set; }
}