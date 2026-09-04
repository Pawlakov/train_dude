// <copyright file="DropCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Admin;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record DropCommand()
    : IEmptyCommand
{
    public const string TypeRoute = "/admin/drop";

    public string Route => TypeRoute;
}