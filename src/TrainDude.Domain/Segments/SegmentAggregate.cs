// <copyright file="SegmentAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Segments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;
using TrainDude.Shared.Values;

public class SegmentAggregate
    : BaseAggregate
{
    private readonly List<Location> course;

    [JsonConstructor]
    private SegmentAggregate(Guid id, long version, double? nominalLength, int tracks, SegmentEnd a, SegmentEnd b, ICollection<Location> course)
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

    public double? NominalLength { get; private set; }

    public int Tracks { get; private set; }

    public SegmentEnd A { get; private set; }

    public SegmentEnd B { get; private set; }

    public ICollection<Location> Course => this.course.AsReadOnly();

    public static SegmentCreated Make(Guid id, double nominalLength, int tracks, SegmentEnd a, SegmentEnd b)
    {
        return new SegmentCreated(id, DateTime.UtcNow, nominalLength, tracks, a, b);
    }

    public SegmentCourseSet SetCourse(IEnumerable<Location> course)
    {
        return new SegmentCourseSet(this.Id, DateTime.UtcNow, course ?? []);
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