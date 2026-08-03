// <copyright file="IReadDbContext.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data.Entities;

public interface IReadDbContext
{
    DbSet<Radius> Radii { get; }

    DbSet<Segment> Segments { get; }

    DbSet<Station> Stations { get; }
}