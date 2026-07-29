// <copyright file="NetworkDbContext.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Data.Models.Configuration;

public partial class NetworkDbContext(DbContextOptions<NetworkDbContext> options)
    : DbContext(options)
{
    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    public virtual DbSet<Radius> Radii { get; set; }

    public virtual DbSet<SegmentExtreme> SegmentExtremes { get; set; }

    public virtual DbSet<Chart> Charts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RadiusEntityTypeConfiguration).Assembly);
    }
}