// <copyright file="Trip.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;

public class Trip
    : IVersionedDocument
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public int TripNumber { get; set; }
}