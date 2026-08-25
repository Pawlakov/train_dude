namespace TrainDude.Domain.Tests;

using TrainDude.Domain.Base;
using TrainDude.Domain.Stations;
using TrainDude.Shared.Values;

using Xunit;

public class Tests
{
    [Fact]
    public void Test()
    {
        var station = new StationAggregate();

        Assert.Throws<UninitializedAggregateException<StationAggregate>>(() => station.SetLocation(new Location(90, 90)));
    }
}