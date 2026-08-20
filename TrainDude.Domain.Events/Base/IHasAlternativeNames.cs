// <copyright file="IHasAlternativeNames.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Base;

public interface IHasAlternativeNames
{
    string NameGerman { get; }

    string? NameGermanNew { get; }

    string? NamePolish { get; }

    string? NameRussian { get; }
}