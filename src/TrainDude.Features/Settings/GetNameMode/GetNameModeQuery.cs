// <copyright file="GetStationNameModeQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.GetNameMode;

using TrainDude.Features.Base;

public sealed record GetNameModeQuery() : BasePolymorphicQuery<GetNameModeQueryResult>;