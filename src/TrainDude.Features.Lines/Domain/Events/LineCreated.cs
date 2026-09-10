// <copyright file="LineCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain.Events;

using System;

public sealed record LineCreated(Guid Id, string Who, int LineNumber, char? LineLetter);