namespace TrainDude.Network.Models;

using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;

internal class Radius
{
    public ObjectId Id { get; set; }

    public int Speed { get; set; }

    public int Minimum { get; set; }
}