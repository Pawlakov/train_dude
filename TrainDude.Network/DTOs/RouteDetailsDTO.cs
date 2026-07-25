namespace TrainDude.Network.DTOs;

public class RouteDetailsDTO
{
    public int Id { get; init; }

    required public StationSummaryDTO A { get; init; }

    required public StationSummaryDTO B { get; init; }
}