namespace TrainDude.Application.Requests.GetSegmentQuery;

using TrainDude.Application.Requests.Base;
using TrainDude.Shared.Values;

public class GetSegmentQueryResult
    : BasePolymorphicResponse
{
    public int SegmentId { get; init; }

    required public string AName { get; init; }

    required public string BName { get; init; }

    required public Location? ALocation { get; set; }

    required public Location? BLocation { get; set; }
}