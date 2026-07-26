namespace TrainDude.Network.DTOs;

using TrainDude.Data.Models;

public class RouteDetailsDTO
{
    public int Id { get; init; }

    required public string AName { get; init; }

    required public string BName { get; init; }

    required public StationLocation ALocation { get; set; }
}