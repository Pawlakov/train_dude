// <copyright file="Settings.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;

using LiteDB;

using TrainDude.Integration.Values;

public class Settings
{
    [BsonId]
    public Guid SettingsId { get; set; }

    public StationNameMode StationNameMode { get; set; }
}