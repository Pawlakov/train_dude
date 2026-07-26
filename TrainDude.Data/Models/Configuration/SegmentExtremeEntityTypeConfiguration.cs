// <copyright file="SegmentExtremeEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class SegmentExtremeEntityTypeConfiguration
    : IEntityTypeConfiguration<SegmentExtreme>
{
    public void Configure(EntityTypeBuilder<SegmentExtreme> builder)
    {
        builder.HasKey(x => new { RouteId = x.SegmentId, IsEnd = x.IsEnd });

        builder
            .HasOne(x => x.Station)
            .WithMany(x => x.SegmentExtremes)
            .HasForeignKey(x => x.StationId)
            .HasPrincipalKey(x => x.StationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Segment)
            .WithMany(x => x.Extremes)
            .HasForeignKey(x => x.SegmentId)
            .HasPrincipalKey(x => x.SegmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}