// <copyright file="SegmentStationReferenceProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Stations.Domain.Events;

public sealed class SegmentStationReferenceProjection
    : SingleStreamProjection<SegmentStationReference, Guid>
{
    public void Apply(IEvent<StationCreated> e, SegmentStationReference aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<StationLocationSet> e, SegmentStationReference aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<StationAxleAdded> e, SegmentStationReference aggregate) => aggregate.Apply(e.Data);
}