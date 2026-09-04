namespace TrainDude.Domain.Tests;

using TrainDude.Infrastructure.Stations;
using TrainDude.Shared.Values;

using Xunit;

public class Tests
{
    [Fact]
    public void Test()
    {
        var station = new StationAggregate();

        Assert.Throws<UninitializedAggregateException>(() => station.SetLocation(new Location(90, 90)));
    }
}