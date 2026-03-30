namespace TrainDude.Network.DTOs;

using MongoDB.Bson;

public class RouteDetailsDTO
{
    public ObjectId Id { get; init; }

    required public StationSummaryDTO A { get; init; }

    required public StationSummaryDTO B { get; init; }
}