// <copyright file="SegmentStationReferenceProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Stations.Domain.Events;

public sealed class LineStationReferenceProjection
    : SingleStreamProjection<LineStationReference, Guid>
{
    public void Apply(IEvent<StationCreated> e, LineStationReference aggregate)
    {
        aggregate.Id = e.Data.StationId;
        aggregate.AxleCount = 1;
        aggregate.NameGerman = e.Data.NameGerman;
        aggregate.NameGermanNew = e.Data.NameGermanNew;
        aggregate.NamePolish = e.Data.NamePolish;
        aggregate.NameRussian = e.Data.NameRussian;
    }

    public void Apply(IEvent<StationLocationSet> e, LineStationReference aggregate)
    {
        aggregate.Location = e.Data.Location;
    }

    public void Apply(IEvent<StationAxleAdded> e, LineStationReference aggregate)
    {
        aggregate.AxleCount += 1;
    }
}