// <copyright file="SegmentEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Data.Entities;

internal sealed class SegmentEntityTypeConfiguration
    : IEntityTypeConfiguration<Segment>
{
    public void Configure(EntityTypeBuilder<Segment> builder)
    {
        builder
            .HasKey(x => x.SegmentId);

        builder
            .Navigation(x => x.Extremes)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Navigation(x => x.Vertices)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .Navigation(x => x.Lines)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}