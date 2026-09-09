// <copyright file="LineTripLink.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.ReadModels;

using System;

public class LineTripLink
{
    public Guid Id { get; set; }

    public Guid TripId { get; set; }
}