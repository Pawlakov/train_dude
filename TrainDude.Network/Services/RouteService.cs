// <copyright file="RouteService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using TrainDude.Network.Models;

internal class RouteService
{
    private readonly IMongoCollection<Route> collection;

    public RouteService(IMongoCollection<Route> collection)
    {
        this.collection = collection;
    }

    public async Task<long> Count()
    {
        return await this.collection.CountDocumentsAsync(FilterDefinition<Route>.Empty);
    }

    public async Task<ObjectId> Insert(Route model)
    {
        await this.collection.InsertOneAsync(model);

        var sameAFilter = Builders<Route>.Filter.Eq(x => x.A, model.A);
        var sameBFilter = Builders<Route>.Filter.Eq(x => x.B, model.B);
        var combinedFilter = Builders<Route>.Filter.And(sameAFilter, sameBFilter);
        var justInserted = await this.collection.Find(combinedFilter).SingleAsync();
        return justInserted.Id;
    }

    public async Task<IEnumerable<Route>> GetAll()
    {
        return await this.collection.Find(FilterDefinition<Route>.Empty).ToListAsync();
    }

    public async Task DeleteAll()
    {
        await this.collection.DeleteManyAsync(FilterDefinition<Route>.Empty);
    }
}