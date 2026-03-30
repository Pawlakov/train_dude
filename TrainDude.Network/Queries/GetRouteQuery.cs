// <copyright file="GetRouteQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

using MongoDB.Bson;

using TrainDude.Network.DTOs;

public class GetRouteQuery : IRequest<RouteDetailsDTO?>
{
    public ObjectId Id { get; }

    public GetRouteQuery(string stringId)
    {
        this.Id = ObjectId.Parse(stringId);
    }
}