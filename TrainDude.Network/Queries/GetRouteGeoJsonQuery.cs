// <copyright file="GetRouteGeoJsonQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

public class GetRouteGeoJsonQuery : IRequest<string>
{
    public int Id { get; }

    public GetRouteGeoJsonQuery(int id)
    {
        this.Id = id;
    }
}