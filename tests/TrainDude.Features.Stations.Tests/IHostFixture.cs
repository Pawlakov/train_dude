// <copyright file="IHostFixture.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Tests;

using Alba;

public interface IHostFixture
{
    IAlbaHost Host { get; }
}