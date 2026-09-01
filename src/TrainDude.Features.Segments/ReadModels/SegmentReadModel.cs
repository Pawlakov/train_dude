// <copyright file="SegmentReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.ReadModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using TrainDude.Features.Segments.Domain.Events;
using TrainDude.Features.Segments.Domain.Values;
using TrainDude.Features.Segments.ReadModels.Events;
using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared;
using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.GetStation;
using TrainDude.Shared.Values;

public sealed class SegmentReadModel
{
    private readonly List<Location> course;

    [JsonConstructor]
    private SegmentReadModel(Guid id, long version, double nominalLength, double? haversine, int tracks, SegmentEnd a, SegmentEnd b, ICollection<Location> course)
    {
        this.Id = id;
        this.Version = version;

        this.NominalLength = nominalLength;
        this.Haversine = haversine;
        this.Tracks = tracks;
        this.course = (course ?? []).ToList();
    }

    public SegmentReadModel()
    {
        this.course = new List<Location>();
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public double NominalLength { get; private set; }

    public double? Haversine { get; private set; }

    public int Tracks { get; private set; }

    public SegmentEndReference A { get; private set; }

    public SegmentEndReference B { get; private set; }

    public IReadOnlyList<Location> Course => this.course.AsReadOnly();

    public void Apply(SegmentCreatedWithReferences e)
    {
        double? haversine = (e.A.Location, e.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => aLocation.Haversine(bLocation),
            _ => null,
        };

        this.Id = e.Id;
        this.NominalLength = e.NominalLength;
        this.Haversine = haversine;
        this.A = e.A;
        this.B = e.B;

        this.Version++;
    }

    public void Apply(SegmentCourseSet e)
    {
        this.course.Clear();
        this.course.AddRange(e.Course);

        this.Haversine = (this.A.Location, this.B.Location) switch
        {
            ({ } aLocation, { } bLocation) => e.Course.Prepend(aLocation).Append(bLocation).Haversine(),
            _ => null,
        };

        this.Version++;
    }

    public void Apply(StationLocationSet e)
    {
        if (this.A.Id == e.Id)
        {
            this.A = this.A with { Location = e.Location };
        }

        if (this.B.Id == e.Id)
        {
            this.B = this.B with { Location = e.Location };
        }
    }

    public void Apply(SettingsNamingPolicySet e)
    {
        throw new NotImplementedException();
    }
}