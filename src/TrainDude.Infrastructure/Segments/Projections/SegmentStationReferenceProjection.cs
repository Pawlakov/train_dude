// <copyright file="SegmentStationReferenceProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Segments.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Segments.ReadModels;
using TrainDude.Features.Stations.Domain.Events;

public sealed class SegmentStationReferenceProjection
    : SingleStreamProjection<SegmentStationReference, Guid>
{
    public void Apply(IEvent<StationCreated> e, SegmentStationReference aggregate)
    {
        aggregate.Id = e.Data.Id;
        aggregate.NameGerman = e.Data.NameGerman;
        aggregate.NameGermanNew = e.Data.NameGermanNew;
        aggregate.NamePolish = e.Data.NamePolish;
        aggregate.NameRussian = e.Data.NameRussian;

        aggregate.Version++;
    }

    public void Apply(IEvent<StationLocationSet> e, SegmentStationReference aggregate)
    {
        aggregate.Location = e.Data.Location;

        aggregate.Version++;
    }

    public void Apply(IEvent<StationAxleAdded> e, SegmentStationReference aggregate)
    {
        aggregate.Version++;
    }
}