// <copyright file="GetStationQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.GetStation;

using System;

using TrainDude.Features.Base;

public sealed record GetStationQuery(Guid StationId) : BaseEntityLookupQuery<GetStationQueryResult>(StationId);