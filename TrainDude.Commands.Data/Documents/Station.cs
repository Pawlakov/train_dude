// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;

using TrainDude.Commands.Data.Events;
using TrainDude.Shared.Values;

public class Station
{
    public Guid Id { get; set; }

    public Location? Location { get; set; }
}