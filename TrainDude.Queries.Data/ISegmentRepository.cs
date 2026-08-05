// <copyright file="ISegmentRepository.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data.Entities;

public interface ISegmentRepository
{
    DbSet<SegmentAggregate> SegmentAggregates { get; }
}