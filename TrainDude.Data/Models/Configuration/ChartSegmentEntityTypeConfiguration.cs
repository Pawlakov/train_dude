// <copyright file="ChartSegmentEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Data.Models.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ChartSegmentEntityTypeConfiguration
    : IEntityTypeConfiguration<ChartSegment>
{
    public void Configure(EntityTypeBuilder<ChartSegment> builder)
    {
        builder.HasKey(x => new { x.SegmentId, x.ChartId });

        builder
            .HasOne(x => x.Chart)
            .WithMany(x => x.Segments)
            .HasForeignKey(x => x.ChartId)
            .HasPrincipalKey(x => x.ChartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Segment)
            .WithMany(x => x.Charts)
            .HasForeignKey(x => x.SegmentId)
            .HasPrincipalKey(x => x.SegmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}