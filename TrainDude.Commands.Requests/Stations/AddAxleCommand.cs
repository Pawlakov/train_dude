// <copyright file="AddAxleCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Stations;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class AddAxleCommand
    : BasePolymorphicCommand
{
    public Guid Id { get; set; }

    public long Version { get; set; }
}