// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;

using TrainDude.Shared.Values;

public class Station
    : IVersionedDocument
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public string Name { get; set; }

    public Location? Location { get; set; }
}