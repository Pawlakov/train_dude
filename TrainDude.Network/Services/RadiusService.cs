// <copyright file="RadiusService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Services;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using TrainDude.Network.Models;

internal class RadiusService
{
    private readonly IMongoCollection<Radius> collection;

    public RadiusService(IMongoCollection<Radius> collection)
    {
        this.collection = collection;
    }

    public async Task<long> Count()
    {
        return await this.collection.CountDocumentsAsync(FilterDefinition<Radius>.Empty);
    }

    public async Task<ObjectId> Insert(Radius model)
    {
        await this.collection.InsertOneAsync(model);

        var sameSpeedFilter = Builders<Radius>.Filter.Eq(x => x.Speed, model.Speed);
        var justInserted = await this.collection.Find(sameSpeedFilter).SingleAsync();
        return justInserted.Id;
    }

    public async Task<IEnumerable<Radius>> GetAll()
    {
        return await this.collection.Find(FilterDefinition<Radius>.Empty).ToListAsync();
    }

    public async Task DeleteAll()
    {
        await this.collection.DeleteManyAsync(FilterDefinition<Radius>.Empty);
    }
}
