// <copyright file="StationProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Projections;

using System;

using Marten.Events.Aggregation;

using TrainDude.Commands.Data.Documents;
using TrainDude.Commands.Data.Events;

public partial class StationProjection
    : SingleStreamProjection<Station, Guid>
{
    public Station Create(StationCreated e)
    {
        return new Station
        {
            Id = e.StationId,
            Location = null,
        };
    }

    public void Apply(Station station, StationLocationSet e)
    {
        station.Location = e.Location;
    }
}