// <copyright file="LineCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Lines;

using System;

public sealed record class LineCreated(Guid Id, int LineNumber, char? LineLetter) : IDomainEvent;