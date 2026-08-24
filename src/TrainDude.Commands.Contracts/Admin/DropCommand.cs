// <copyright file="DropCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Admin;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;

public sealed record class DropCommand
    : BaseRoutedCommand<EmptyResponse>
{
    public const string Route = "/admin/drop";

    public DropCommand()
        : base(Route)
    {
    }
}