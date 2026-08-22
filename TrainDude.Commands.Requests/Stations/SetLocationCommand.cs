// <copyright file="SetLocationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Stations;

using System;

using TrainDude.Commands.Requests.Base;
using TrainDude.Integration.Values;

public sealed record class SetLocationCommand
    : BaseRoutedCommand
{
    public const string Route = "station/location/set";

    public SetLocationCommand()
        : base(Route)
    {
    }

    public Guid Id { get; set; }

    public long Version { get; set; }

    public Location Location { get; set; }
}