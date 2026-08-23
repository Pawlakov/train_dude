// <copyright file="BaseRoutedCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Base;

using System.Text.Json.Serialization;

public abstract record class BaseRoutedCommand<TResponse>
    : BasePolymorphicCommand
    where TResponse : BaseCommandResponse
{
    protected BaseRoutedCommand(string route)
    {
        this.Route = route;
    }

    [JsonIgnore]
    public string Route { get; }
}
