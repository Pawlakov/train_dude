// <copyright file="GetSegmentGeoDataQueryResultSegmentItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentGeoJsonQuery;

using TrainDude.Shared.Values;

public class GetSegmentGeoDataQueryResultSegmentItem
{
    public Location ALocation { get; set; }

    public Location BLocation { get; set; }

    public ICollection<Location> Vertices { get; set; }
}