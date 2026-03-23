using Itmo.ObjectOrientedProgramming.Lab1.RouteModule;
using Itmo.ObjectOrientedProgramming.Lab1.RouteSegmentModule;
using Itmo.ObjectOrientedProgramming.Lab1.TrainModule;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class TrainTests
{
    [Fact]
    public void Scenario1_RouteWithAccelerationAndRegularSegment_ShouldSucceed()
    {
        var train = new Train(1000, 5000);
        var route = new Route(100);

        route.AddSegment(new ForceSegment(300, 1000));

        route.AddSegment(new RegularSegment(200));

        bool result = route.TryPassRoute(train, out double totalTime);

        Assert.True(result);
        Assert.True(totalTime > 0);
    }

    [Fact]
    public void Scenario2_RouteWithExcessiveAcceleration_ShouldFail()
    {
        var train = new Train(1000, 5000);
        var route = new Route(30);

        route.AddSegment(new ForceSegment(2000, 4000));
        route.AddSegment(new RegularSegment(500));

        bool result = route.TryPassRoute(train, out _);

        Assert.False(result);
    }

    [Fact]
    public void Scenario3_RouteWithStation_ShouldSucceed()
    {
        var train = new Train(1000, 5000);
        var route = new Route(100);

        route.AddSegment(new ForceSegment(300, 800));
        route.AddSegment(new RegularSegment(50));
        route.AddSegment(new StationSegment(40, 5, 15));
        route.AddSegment(new RegularSegment(50));

        bool result = route.TryPassRoute(train, out double totalTime);

        Assert.True(result);
    }

    [Fact]
    public void Scenario4_RouteWithExcessiveSpeedForStation_ShouldFail()
    {
        var train = new Train(1000, 5000);
        var route = new Route(50);

        route.AddSegment(new ForceSegment(1500, 4500));
        route.AddSegment(new StationSegment(20, 10, 15));
        route.AddSegment(new RegularSegment(200));

        bool result = route.TryPassRoute(train, out _);

        Assert.False(result);
    }

    [Fact]
    public void Scenario5_RouteWithExcessiveSpeedForRouteLimit_ShouldFail()
    {
        var train = new Train(1000, 5000);
        var route = new Route(25);

        route.AddSegment(new ForceSegment(1000, 3000));
        route.AddSegment(new RegularSegment(300));
        route.AddSegment(new StationSegment(1000, 10, 10));
        route.AddSegment(new RegularSegment(400));

        bool result = route.TryPassRoute(train, out _);

        Assert.False(result);
    }

    [Fact]
    public void Scenario6_ComplexRouteWithBraking_ShouldSucceed()
    {
        var train = new Train(1000, 5000);
        var route = new Route(35);

        route.AddSegment(new ForceSegment(300, 1000));
        route.AddSegment(new RegularSegment(300));
        route.AddSegment(new ForceSegment(300, -800));
        route.AddSegment(new StationSegment(25, 15, 15));
        route.AddSegment(new RegularSegment(400));
        route.AddSegment(new ForceSegment(800, 3500));
        route.AddSegment(new RegularSegment(200));
        route.AddSegment(new ForceSegment(800, -3500));

        bool result = route.TryPassRoute(train, out double totalTime);

        Assert.True(result);
    }

    [Fact]
    public void Scenario7_RegularSegmentWithoutMovement_ShouldFail()
    {
        var train = new Train(1000, 5000);
        var route = new Route(50);

        route.AddSegment(new RegularSegment(1000));

        bool result = route.TryPassRoute(train, out _);

        Assert.False(result);
    }

    [Fact]
    public void Scenario8_ForceSegmentsWithExcessiveBraking_ShouldFail()
    {
        var train = new Train(1000, 7000);
        var route = new Route(50);

        route.AddSegment(new ForceSegment(1000, 3000));

        route.AddSegment(new ForceSegment(1000, -6000));

        bool result = route.TryPassRoute(train, out _);

        Assert.False(result);
    }
}