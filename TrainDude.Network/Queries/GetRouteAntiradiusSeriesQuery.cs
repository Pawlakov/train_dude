// <copyright file="GetRouteAntiradiusSeriesQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

using MongoDB.Bson;

public class GetRouteAntiradiusSeriesQuery : IRequest<string>
{
    public ObjectId Id { get; }

    public int Resolution { get; }

    public GetRouteAntiradiusSeriesQuery(string stringId, int resolution)
    {
        this.Id = ObjectId.Parse(stringId);
        this.Resolution = resolution;
    }
}