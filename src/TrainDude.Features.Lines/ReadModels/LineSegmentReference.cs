// <copyright file="LineSegmentReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Segments.Domain.Events;

public sealed class LineSegmentReference
{
    [JsonConstructor]
    public LineSegmentReference(Guid id, long version)
    {
        this.Id = id;
        this.Version = version;
    }

    public LineSegmentReference()
    {
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public void Apply(SegmentCreated e)
    {
        this.Id = e.Id;

        this.Version++;
    }
}