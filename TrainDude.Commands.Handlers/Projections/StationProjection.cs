// <copyright file="StationProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Documents;
using TrainDude.Domain.Events.Stations;

public partial class StationProjection
    : SingleStreamProjection<Station, Guid>
{
    public void Apply(IEvent<StationCreated> e, Station station) => station.Apply(e.Data);

    public void Apply(IEvent<StationLocationSet> e, Station station) => station.Apply(e.Data);

    public void Apply(IEvent<StationAxleAdded> e, Station station) => station.Apply(e.Data);
}