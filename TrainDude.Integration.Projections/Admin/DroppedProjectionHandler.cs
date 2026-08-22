// <copyright file="DroppedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Admin;

using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Settings;

public static class DroppedProjectionHandler
{
    public static Task Handle(DroppedIntegrationEvent @event, ILiteDatabase db)
    {
        foreach (var collectionName in db.GetCollectionNames())
        {
            db.GetCollection(collectionName).DeleteAll();
        }

        return Task.CompletedTask;
    }
}