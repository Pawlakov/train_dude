// <copyright file="GetLineQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLine;

using System;

using TrainDude.Features.Base;

public sealed record GetLineQuery(Guid LineId) : BaseEntityLookupQuery<GetLineQueryResult>(LineId);