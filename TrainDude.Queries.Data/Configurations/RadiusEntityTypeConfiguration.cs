// <copyright file="RadiusEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Queries.Data.Entities;

internal sealed class RadiusEntityTypeConfiguration
    : IEntityTypeConfiguration<RadiusAggregate>
{
    public void Configure(EntityTypeBuilder<RadiusAggregate> builder)
    {
        builder.HasKey(x => x.RadiusId);

        builder
            .Property(x => x.RadiusId)
            .ValueGeneratedNever();
    }
}