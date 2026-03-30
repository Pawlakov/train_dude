// <copyright file="GetRouteGeoJsonQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using MediatR;

using MongoDB.Bson;

public class GetRouteGeoJsonQuery : IRequest<string>
{
    public ObjectId Id { get; }

    public GetRouteGeoJsonQuery(string stringId)
    {
        this.Id = ObjectId.Parse(stringId);
    }
}