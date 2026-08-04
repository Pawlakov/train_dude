// <copyright file="LineEntityTypeConfiguration.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TrainDude.Commands.Data.Entities;

internal sealed class LineEntityTypeConfiguration
    : IEntityTypeConfiguration<Line>
{
    public void Configure(EntityTypeBuilder<Line> builder)
    {
        builder
            .HasKey(x => new { x.LineNumber, x.LineLetter });

        builder
            .Property(x => x.LineNumber)
            .ValueGeneratedNever();

        builder
            .Property(x => x.LineLetter)
            .ValueGeneratedNever();
    }
}