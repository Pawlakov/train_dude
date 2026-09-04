// <copyright file="LineSegmentReferenceProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Segments.Domain.Events;

public sealed class LineSegmentReferenceProjection
    : SingleStreamProjection<LineSegmentReference, Guid>
{
    public void Apply(IEvent<SegmentCreated> e, LineSegmentReference readModel) => readModel.Apply(e.Data);
}