// <copyright file="Chart.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models;

using System.Collections.Generic;

public class Chart
{
    public string ChartId { get; set; }

    public virtual ICollection<ChartSegment> Segments { get; set; }
}