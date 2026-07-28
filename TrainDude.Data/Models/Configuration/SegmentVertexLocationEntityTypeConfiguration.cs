// <copyright file="SegmentVertexLocationEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SegmentVertexLocationEntityTypeConfiguration
    : IEntityTypeConfiguration<SegmentVertexLocation>
{
    public void Configure(EntityTypeBuilder<SegmentVertexLocation> builder)
    {
        builder.HasKey(x => new { x.SegmentId, x.OrdinalId });

        builder
            .HasOne(x => x.Segment)
            .WithMany(x => x.Vertices)
            .HasForeignKey(x => x.SegmentId)
            .HasPrincipalKey(x => x.SegmentId);
    }
}