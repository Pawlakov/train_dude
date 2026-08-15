// <copyright file="StationCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Stations;

using System;

public sealed record class StationCreated(Guid Id, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : IDomainEvent;