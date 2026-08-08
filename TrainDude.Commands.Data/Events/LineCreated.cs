// <copyright file="LineCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Events;

using System;

public record class LineCreated(Guid LineId, int LineNumber, char? LineLetter);