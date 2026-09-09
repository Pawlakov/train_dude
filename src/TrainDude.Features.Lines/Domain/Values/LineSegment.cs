// <copyright file="LineSegment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain.Values;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Values;

public sealed record LineSegment(Guid Id, IReadOnlyList<Location>? FullCourse, LineStation A, LineStation B);