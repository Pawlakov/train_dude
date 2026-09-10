// <copyright file="SegmentAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Shared.Contracts.Values;

public class SegmentAggregate
{
    private readonly List<Location> course;

    [JsonConstructor]
    private SegmentAggregate(Guid id, long version, double nominalLength, int tracks, SegmentEnd a, SegmentEnd b, ICollection<Location> course)
    {
        this.Id = id;
        this.Version = version;

        this.NominalLength = nominalLength;
        this.Tracks = tracks;
        this.A = a;
        this.B = b;
        this.course = (course ?? []).ToList();
    }

    public SegmentAggregate()
    {
        this.course = new List<Location>();
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public double NominalLength { get; private set; }

    public int Tracks { get; private set; }

    public SegmentEnd A { get; private set; }

    public SegmentEnd B { get; private set; }

    public IReadOnlyList<Location> Course => this.course.AsReadOnly();

    public static SegmentCreated Make(Guid id, string who, double nominalLength, int tracks, SegmentEnd a, SegmentEnd b)
    {
        return new SegmentCreated(id, who, nominalLength, tracks, a, b);
    }

    public SegmentCourseSet SetCourse(string who, IEnumerable<Location> course)
    {
        return new SegmentCourseSet(this.Id, who, course ?? []);
    }

    public void Apply(SegmentCreated e)
    {
        this.Id = e.Id;
        this.NominalLength = e.NominalLength;
        this.Tracks = e.Tracks;
        this.A = e.A;
        this.B = e.B;

        this.Version++;
    }

    public void Apply(SegmentCourseSet e)
    {
        this.course.Clear();
        this.course.AddRange(e.Course);

        this.Version++;
    }
}