// <copyright file="RadiusEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Data.Entities;

internal sealed class RadiusEntityTypeConfiguration
    : IEntityTypeConfiguration<Radius>
{
    public void Configure(EntityTypeBuilder<Radius> builder)
    {
        builder.HasKey(x => x.RadiusId);
    }
}