// <copyright file="Radius.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Entities;

public class Radius
{
    private Radius()
    {
    }

    public int RadiusId { get; private set; }

    public int Speed { get; private set; }

    public int Minimum { get; private set; }
}