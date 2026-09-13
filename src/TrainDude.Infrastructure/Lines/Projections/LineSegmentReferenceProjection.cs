// <copyright file="LineSegmentReferenceProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Projections;

using System;
using System.Linq;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Segments.Domain.Events;

public sealed class LineSegmentReferenceProjection
    : SingleStreamProjection<LineSegmentReference, Guid>
{
    public void Apply(IEvent<SegmentCreated> e, LineSegmentReference aggregate)
    {
        aggregate.Id = e.Data.SegmentId;
        aggregate.AId = e.Data.A.Id;
        aggregate.BId = e.Data.B.Id;
        aggregate.Course = [];
    }

    public void Apply(IEvent<SegmentCourseSet> e, LineSegmentReference aggregate)
    {
        aggregate.Course = e.Data.Course.ToList();
    }
}