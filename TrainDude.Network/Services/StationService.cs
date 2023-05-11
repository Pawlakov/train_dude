// <copyright file="StationService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using TrainDude.Network.Models;

internal class StationService
{
    private readonly IMongoCollection<Station> collection;

    public StationService(IMongoCollection<Station> collection)
    {
        this.collection = collection;
    }

    public async Task<long> Count()
    {
        return await this.collection.CountDocumentsAsync(FilterDefinition<Station>.Empty);
    }

    public async Task<ObjectId> Insert(Station model)
    {
        await this.collection.InsertOneAsync(model);

        var sameNameFilter = Builders<Station>.Filter.Eq(x => x.NameGerman, model.NameGerman);
        var sameLocationFilter = Builders<Station>.Filter.Eq(x => x.Location, model.Location);
        var combinedFilter = Builders<Station>.Filter.And(sameNameFilter, sameLocationFilter);
        var justInserted = await this.collection.Find(combinedFilter).SingleAsync();
        return justInserted.Id;
    }

    public async Task<IEnumerable<Station>> GetAll()
    {
        return await this.collection.Find(FilterDefinition<Station>.Empty).ToListAsync();
    }

    public async Task DeleteAll()
    {
        await this.collection.DeleteManyAsync(FilterDefinition<Station>.Empty);
    }
}