// <copyright file="StationCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Stations;

using System;

using TrainDude.Domain.Events.Base;

public sealed record class StationCreated(Guid Id, DateTime When, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : BaseAggregateEvent(Id, When), IHasAlternativeNames;