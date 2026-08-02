// <copyright file="StationEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Data.Entities;

internal sealed class StationEntityTypeConfiguration
    : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder
            .HasKey(x => x.StationId);

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

        builder
            .Navigation(x => x.SegmentExtremes)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}