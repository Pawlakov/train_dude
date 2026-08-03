// <copyright file="SegmentStationEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Queries.Data.Entities;

internal sealed class SegmentStationEntityTypeConfiguration
    : IEntityTypeConfiguration<SegmentStation>
{
    public void Configure(EntityTypeBuilder<SegmentStation> builder)
    {
        builder.HasKey(x => x.SegmentStationId);

        builder
            .HasIndex(x => x.NameGerman)
            .IsUnique();

        builder
            .ComplexProperty(
            x => x.Location,
            x =>
            {
                x.IsRequired(false);
                x.Property(y => y.Latitude).IsRequired();
                x.Property(y => y.Longitude).IsRequired();
            });
    }
}