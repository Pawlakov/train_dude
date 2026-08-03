// <copyright file="IWriteDbContext.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Data;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TrainDude.Commands.Data.Entities;

public interface IWriteDbContext
{
    DbSet<Line> Lines { get; }

    DbSet<Radius> Radii { get; }

    DbSet<Segment> Segments { get; }

    DbSet<Station> Stations { get; }

    DbSet<Trip> Trips { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}