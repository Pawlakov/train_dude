// <copyright file="SegmentVertexLocationEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Data.Entities;

internal sealed class SegmentVertexEntityTypeConfiguration
    : IEntityTypeConfiguration<SegmentVertex>
{
    public void Configure(EntityTypeBuilder<SegmentVertex> builder)
    {
        builder.HasKey(x => new { x.SegmentId, x.OrdinalId });

        builder
            .HasOne(x => x.Segment)
            .WithMany(x => x.Vertices)
            .HasForeignKey(x => x.SegmentId)
            .HasPrincipalKey(x => x.SegmentId);

        builder
            .ComplexProperty(
            x => x.Location,
            x =>
            {
                x.IsRequired();
                x.Property(y => y.Latitude).IsRequired();
                x.Property(y => y.Longitude).IsRequired();
            });
    }
}