// <copyright file="LineCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;

using TrainDude.Domain.Base;

public sealed record class LineCreated(Guid Id, DateTime When, int LineNumber, char? LineLetter) : BaseAggregateEvent(Id, When);