// <copyright file="IStationRepository.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data.Entities;

public interface IStationRepository
{
    DbSet<Station> Stations { get; }
}