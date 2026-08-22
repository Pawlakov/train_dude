// <copyright file="Settings.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;

using TrainDude.Shared.Values;

public class Settings
{
    public Guid Id { get; set; }

    public StationNameMode StationNameMode { get; set; }
}