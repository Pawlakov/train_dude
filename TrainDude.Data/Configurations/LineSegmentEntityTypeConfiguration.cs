// <copyright file="LineSegmentEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Data.Entities;

internal sealed class LineSegmentEntityTypeConfiguration
    : IEntityTypeConfiguration<LineSegment>
{
    public void Configure(EntityTypeBuilder<LineSegment> builder)
    {
        builder.HasKey(x => new { x.SegmentId, x.LineNumber, x.LineLetter });

        builder
            .HasOne(x => x.Line)
            .WithMany(x => x.Segments)
            .HasForeignKey(x => new { x.LineNumber, x.LineLetter })
            .HasPrincipalKey(x => new { x.LineNumber, x.LineLetter })
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Segment)
            .WithMany(x => x.Lines)
            .HasForeignKey(x => x.SegmentId)
            .HasPrincipalKey(x => x.SegmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}