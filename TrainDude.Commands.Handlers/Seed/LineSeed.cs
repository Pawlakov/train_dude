// <copyright file="LineSeed.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Seed;

internal class LineSeed
{
    public int Number { get; set; }

    public char? Letter { get; set; }

    public int RootStation { get; set; }

    public int[] Stations { get; set; }

    public int[] Trips { get; set; }
}