// <copyright file="StationLocationEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StationLocationEntityTypeConfiguration
    : IEntityTypeConfiguration<StationLocation>
{
    public void Configure(EntityTypeBuilder<StationLocation> builder)
    {
        builder.HasKey(x => x.StationId);

        builder
            .HasOne(x => x.Station)
            .WithOne(x => x.Location)
            .HasForeignKey<StationLocation>(x => x.StationId)
            .HasPrincipalKey<Station>(x => x.StationId);
    }
}