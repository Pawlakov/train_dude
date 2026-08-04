// <copyright file="ReadDbContext.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data.Configurations;
using TrainDude.Queries.Data.Entities;

internal sealed class ReadDbContext(DbContextOptions<ReadDbContext> options)
    : DbContext(options), IRadiusRepository, ISegmentRepository, IStationRepository
{
    public DbSet<Radius> Radii => this.Set<Radius>();

    public DbSet<Segment> Segments => this.Set<Segment>();

    public DbSet<Station> Stations => this.Set<Station>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RadiusEntityTypeConfiguration).Assembly);
    }
}