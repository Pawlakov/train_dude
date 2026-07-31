// <copyright file="ChartSegmentEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Data.Entities.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class LineSegmentEntityTypeConfiguration
    : IEntityTypeConfiguration<LineSegment>
{
    public void Configure(EntityTypeBuilder<LineSegment> builder)
    {
        builder.HasKey(x => new { x.SegmentId, ChartId = x.LineId });

        builder
            .HasOne(x => x.Line)
            .WithMany(x => x.Segments)
            .HasForeignKey(x => x.LineId)
            .HasPrincipalKey(x => x.LineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Segment)
            .WithMany(x => x.Lines)
            .HasForeignKey(x => x.SegmentId)
            .HasPrincipalKey(x => x.SegmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}