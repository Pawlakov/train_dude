// <copyright file="SegmentExtremeEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Commands.Data.Entities;

internal sealed class SegmentExtremeEntityTypeConfiguration
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