// <copyright file="LineTripLinkProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Infrastructure.Lines.ReadModels;

public class LineTripLinkProjection
    : SingleStreamProjection<LineTripLink, Guid>
{
    public void Apply(IEvent<LineTripAssigned> e, LineTripLink aggregate)
    {
        aggregate.Id = e.Data.Id;
        aggregate.TripId = e.Data.TripId;
    }
}