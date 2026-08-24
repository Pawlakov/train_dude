// <copyright file="IVersionedDocument.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data;

using System;

public interface IVersionedDocument
{
    public Guid Id { get; set; }

    public long Version { get; set; }
}