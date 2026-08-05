// <copyright file="ReadDbContext.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using TrainDude.Queries.Data.Configurations;
using TrainDude.Queries.Data.Entities;

internal sealed class ReadDbContext(DbContextOptions<ReadDbContext> options)
    : DbContext(options), IReadDbContext
{
    public DbSet<RadiusAggregate> RadiusAggregates => this.Set<RadiusAggregate>();

    public DbSet<SegmentAggregate> SegmentAggregates => this.Set<SegmentAggregate>();

    public DbSet<StationAggregate> StationAggregates => this.Set<StationAggregate>();

    public DbSet<StationEntity> StationEntities => this.Set<StationEntity>();

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return this.Database.BeginTransactionAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RadiusEntityTypeConfiguration).Assembly);
    }
}