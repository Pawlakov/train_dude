// <copyright file="SegmentEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Queries.Data.Entities;

public class SegmentEntityTypeConfiguration
    : IEntityTypeConfiguration<SegmentAggregate>
{
    public void Configure(EntityTypeBuilder<SegmentAggregate> builder)
    {
        builder.HasKey(x => x.SegmentId);

        builder
            .Property(x => x.SegmentId)
            .ValueGeneratedNever();
    }
}