// <copyright file="TripEntityConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Data.Entities;

internal sealed class TripEntityConfiguration
    : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(x => x.TripNumber);
        builder.Property(x => x.TripNumber).ValueGeneratedNever();
    }
}