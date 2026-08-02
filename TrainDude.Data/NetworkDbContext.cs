// <copyright file="NetworkDbContext.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Configurations;
using TrainDude.Data.Entities;

internal sealed class NetworkDbContext(DbContextOptions<NetworkDbContext> options)
    : DbContext(options), INetworkDbContext
{
    public DbSet<Line> Lines => this.Set<Line>();

    public DbSet<Radius> Radii => this.Set<Radius>();

    public DbSet<Segment> Segments => this.Set<Segment>();

    public DbSet<Station> Stations => this.Set<Station>();

    public DbSet<Trip> Trips => this.Set<Trip>();

    private DbSet<LineSegment> LineSegments => this.Set<LineSegment>();

    private DbSet<SegmentExtreme> SegmentExtremes => this.Set<SegmentExtreme>();

    private DbSet<SegmentVertex> SegmentVertices => this.Set<SegmentVertex>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RadiusEntityTypeConfiguration).Assembly);
    }
}