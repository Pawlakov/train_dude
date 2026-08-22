// <copyright file="DropCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Admin;

using TrainDude.Commands.Requests.Base;

public sealed record class DropCommand
    : BaseRoutedCommand
{
    public const string Route = "admin/drop";

    public DropCommand()
        : base(Route)
    {
    }
}