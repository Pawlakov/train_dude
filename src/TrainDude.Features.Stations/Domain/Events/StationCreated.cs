// <copyright file="StationCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain.Events;

using System;

using TrainDude.Features.Shared.Base;
using TrainDude.Features.Shared.Contracts.Base;

public sealed record StationCreated(Guid StationId, string Who, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian)
    : IDomainEvent, IHasAlternativeNames
{
    public Guid Id => this.StationId;
}