// <copyright file="LineTripReferenceProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Trips.Domain.Events;

public sealed class LineTripReferenceProjection
    : SingleStreamProjection<LineTripReference, Guid>
{
    public void Apply(IEvent<TripCreated> e, LineTripReference readModel)
    {
        readModel.Id = e.Id;
        readModel.Number = e.Data.TripNumber;
    }
}