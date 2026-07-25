// <copyright file="GetRouteAntiradiusSeriesQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

public class GetRouteAntiradiusSeriesQuery
    : IRequest<string>
{
    public int Id { get; }

    public int Resolution { get; }

    public GetRouteAntiradiusSeriesQuery(int id, int resolution)
    {
        this.Id = id;
        this.Resolution = resolution;
    }
}