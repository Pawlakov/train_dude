// <copyright file="GetRouteQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

using TrainDude.Network.DTOs;

public class GetRouteQuery : IRequest<RouteDetailsDTO?>
{
    public int Id { get; }

    public GetRouteQuery(int stringId)
    {
        this.Id = stringId;
    }
}