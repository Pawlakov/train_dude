// <copyright file="StationCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Stations;

using System;

using TrainDude.Domain.Base;

public sealed record class StationCreated(Guid Id, DateTime When, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : BaseAggregateEvent(Id, When), IHasAlternativeNames;