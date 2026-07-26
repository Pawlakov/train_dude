// <copyright file="RadiusEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class RadiusEntityTypeConfiguration
    : IEntityTypeConfiguration<Radius>
{
    public void Configure(EntityTypeBuilder<Radius> builder)
    {
        builder.HasKey(x => x.RadiusId);
    }
}