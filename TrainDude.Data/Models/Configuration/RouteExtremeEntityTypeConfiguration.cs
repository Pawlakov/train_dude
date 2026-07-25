// <copyright file="RouteExtremeEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class RouteExtremeEntityTypeConfiguration
    : IEntityTypeConfiguration<RouteExtreme>
{
    public void Configure(EntityTypeBuilder<RouteExtreme> builder)
    {
        builder.ToTable("RouteExtremes");

        builder.HasKey(x => new { x.RouteId, IsEnd = x.IsEnd });

        builder
            .HasOne(x => x.Station)
            .WithMany(x => x.RouteEnds)
            .HasForeignKey(x => x.StationId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Route)
            .WithMany(x => x.Ends)
            .HasForeignKey(x => x.RouteId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}