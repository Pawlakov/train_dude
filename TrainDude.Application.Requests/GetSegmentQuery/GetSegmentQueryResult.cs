namespace TrainDude.Application.Requests.GetSegmentQuery;

using TrainDude.Application.Requests.Values;

public class GetSegmentQueryResult
{
    public int SegmentId { get; init; }

    required public string AName { get; init; }

    required public string BName { get; init; }
    
    required public GeodeticPosition ALocation { get; set; }

    required public GeodeticPosition BLocation { get; set; }
}