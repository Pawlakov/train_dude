// <copyright file="BaseRoutedCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Base;

using System.Text.Json.Serialization;

using TrainDude.Commands.Contracts.Admin;

public abstract record class BaseRoutedCommand
{
    protected BaseRoutedCommand(string route)
    {
        this.Route = route;
    }

    [JsonIgnore]
    public string Route { get; }
}